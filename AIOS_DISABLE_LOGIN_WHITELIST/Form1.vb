Imports Renci.SshNet
Imports System.IO

Public Class Form1
    ' Αρχείο αποθήκευσης ρυθμίσεων
    Private settingsFilePath As String = "settings.enc"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Αρχικά όλα τα πεδία είναι μη εγγράψιμα
        SetFieldsReadOnly(True)

        ' Φόρτωση των αποθηκευμένων ρυθμίσεων κατά την εκκίνηση
        LoadSettings()

        ' Εμφάνιση της τρέχουσας τιμής του AIOS_DISABLE_LOGIN_WHITELIST αν υπάρχουν αποθηκευμένες ρυθμίσεις
        If Not String.IsNullOrEmpty(TextBoxHost.Text) AndAlso Not String.IsNullOrEmpty(TextBoxFilePath.Text) Then
            ShowCurrentSettingValue(TextBoxHost.Text, TextBoxUsername.Text, RichTextBoxPrivateKey.Text, TextBoxFilePath.Text)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Πάρε τις πληροφορίες από τα πεδία
        Dim host As String = TextBoxHost.Text
        Dim username As String = TextBoxUsername.Text
        Dim privateKey As String = RichTextBoxPrivateKey.Text
        Dim filePath As String = TextBoxFilePath.Text

        ' Αποθήκευση των πληροφοριών κωδικοποιημένων
        SaveSettings(host, username, privateKey, filePath)

        ' Εκτέλεση της αλλαγής στο αρχείο wp-config.php
        Try
            ModifyWpConfig(host, username, privateKey, filePath)
            ' Ενημέρωση της τρέχουσας τιμής μετά την αλλαγή
            ShowCurrentSettingValue(host, username, privateKey, filePath)
            MessageBox.Show("Η αλλαγή ολοκληρώθηκε με επιτυχία.")
        Catch ex As Exception
            MessageBox.Show("Αποτυχία σύνδεσης: " & ex.Message)
        End Try

        ' Κάνε τα πεδία ξανά μη εγγράψιμα
        SetFieldsReadOnly(True)
    End Sub

    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click
        ' Κάνε τα πεδία εγγράψιμα
        SetFieldsReadOnly(False)
    End Sub

    Private Sub SetFieldsReadOnly(isReadOnly As Boolean)
        ' Ορισμός της κατάστασης εγγραφής των πεδίων
        TextBoxHost.ReadOnly = isReadOnly
        TextBoxUsername.ReadOnly = isReadOnly
        RichTextBoxPrivateKey.ReadOnly = isReadOnly
        TextBoxFilePath.ReadOnly = isReadOnly
    End Sub

    Private Sub SaveSettings(host As String, username As String, privateKey As String, filePath As String)
        ' Συνένωση των δεδομένων σε ένα string, διαχωρισμένα με ένα ξεχωριστό σύμβολο
        Dim data As String = $"{host}|||{username}|||{privateKey}|||{filePath}"

        ' Κωδικοποίηση σε Base64
        Dim encodedData As String = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(data))

        ' Αποθήκευση σε αρχείο
        File.WriteAllText(settingsFilePath, encodedData)
    End Sub

    Private Sub LoadSettings()
        ' Έλεγχος αν το αρχείο υπάρχει
        If Not File.Exists(settingsFilePath) Then
            Return ' Αν δεν υπάρχει, δεν κάνουμε τίποτα
        End If

        ' Ανάγνωση και αποκωδικοποίηση των δεδομένων από το αρχείο
        Dim encodedData As String = File.ReadAllText(settingsFilePath)
        Dim decodedData As String = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedData))

        ' Διαχωρισμός των δεδομένων
        Dim parts As String() = decodedData.Split(New String() {"|||"}, StringSplitOptions.None)
        If parts.Length = 4 Then
            TextBoxHost.Text = parts(0)
            TextBoxUsername.Text = parts(1)
            RichTextBoxPrivateKey.Text = parts(2)
            TextBoxFilePath.Text = parts(3)
        End If
    End Sub

    Private Sub ShowCurrentSettingValue(host As String, username As String, privateKey As String, filePath As String)
        Try
            ' Δημιουργία του PrivateKeyFile από το string
            Using privateKeyStream As New IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(privateKey))
                Dim privateKeyFile As New PrivateKeyFile(privateKeyStream)
                Dim keyFiles As PrivateKeyFile() = {privateKeyFile}
                Dim methods As AuthenticationMethod() = {New PrivateKeyAuthenticationMethod(username, keyFiles)}

                ' Ρυθμίσεις σύνδεσης
                Dim connectionInfo As New ConnectionInfo(host, 22, username, methods)

                ' Σύνδεση μέσω SSH
                Using client As New SshClient(connectionInfo)
                    client.Connect()

                    ' Ανάγνωση του αρχείου wp-config.php
                    Dim commandRead As SshCommand = client.CreateCommand($"cat {filePath}")
                    Dim fileContent As String = commandRead.Execute()

                    ' Έλεγχος της τρέχουσας τιμής του AIOS_DISABLE_LOGIN_WHITELIST
                    If fileContent.Contains("define('AIOS_DISABLE_LOGIN_WHITELIST', false);") Then
                        LabelCurrentValue.Text = "Η τρέχουσα τιμή είναι: false"
                    ElseIf fileContent.Contains("define('AIOS_DISABLE_LOGIN_WHITELIST', true);") Then
                        LabelCurrentValue.Text = "Η τρέχουσα τιμή είναι: true"
                    Else
                        LabelCurrentValue.Text = "Η τρέχουσα τιμή δεν βρέθηκε."
                    End If

                    client.Disconnect()
                End Using
            End Using
        Catch ex As Exception
            LabelCurrentValue.Text = "Σφάλμα κατά την ανάγνωση της τρέχουσας τιμής."
        End Try
    End Sub

    Private Sub ModifyWpConfig(host As String, username As String, privateKey As String, filePath As String)
        ' Δημιουργία του PrivateKeyFile από το string
        Using privateKeyStream As New IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(privateKey))
            Dim privateKeyFile As New PrivateKeyFile(privateKeyStream)
            Dim keyFiles As PrivateKeyFile() = {privateKeyFile}
            Dim methods As AuthenticationMethod() = {New PrivateKeyAuthenticationMethod(username, keyFiles)}

            ' Ρυθμίσεις σύνδεσης
            Dim connectionInfo As New ConnectionInfo(host, 22, username, methods)

            ' Σύνδεση μέσω SSH
            Using client As New SshClient(connectionInfo)
                client.Connect()

                ' Ανάγνωση του αρχείου wp-config.php
                Dim commandRead As SshCommand = client.CreateCommand($"cat {filePath}")
                Dim fileContent As String = commandRead.Execute()

                ' Τροποποίηση του περιεχομένου
                If fileContent.Contains("define('AIOS_DISABLE_LOGIN_WHITELIST', false);") Then
                    fileContent = fileContent.Replace("define('AIOS_DISABLE_LOGIN_WHITELIST', false);", "define('AIOS_DISABLE_LOGIN_WHITELIST', true);")
                ElseIf fileContent.Contains("define('AIOS_DISABLE_LOGIN_WHITELIST', true);") Then
                    fileContent = fileContent.Replace("define('AIOS_DISABLE_LOGIN_WHITELIST', true);", "define('AIOS_DISABLE_LOGIN_WHITELIST', false);")
                Else
                    Throw New Exception("Η συγκεκριμένη γραμμή δεν βρέθηκε στο αρχείο.")
                End If

                ' Αποθήκευση του τροποποιημένου περιεχομένου σε προσωρινό αρχείο και μεταφόρτωση
                Dim tempLocalFilePath As String = Path.GetTempFileName()
                File.WriteAllText(tempLocalFilePath, fileContent)

                Using sftp As New SftpClient(connectionInfo)
                    sftp.Connect()
                    Using fileStream As New FileStream(tempLocalFilePath, FileMode.Open)
                        sftp.UploadFile(fileStream, filePath, True)
                    End Using
                    sftp.Disconnect()
                End Using

                File.Delete(tempLocalFilePath)
                client.Disconnect()
            End Using
        End Using
    End Sub
End Class
