<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Button1 = New Button()
        TextBoxHost = New TextBox()
        TextBoxUsername = New TextBox()
        TextBoxFilePath = New TextBox()
        RichTextBoxPrivateKey = New RichTextBox()
        LabelCurrentValue = New Label()
        ButtonEdit = New Button()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(55, 320)
        Button1.Name = "Button1"
        Button1.Size = New Size(112, 34)
        Button1.TabIndex = 0
        Button1.Text = "Send"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' TextBoxHost
        ' 
        TextBoxHost.Location = New Point(55, 46)
        TextBoxHost.Name = "TextBoxHost"
        TextBoxHost.ReadOnly = True
        TextBoxHost.Size = New Size(150, 31)
        TextBoxHost.TabIndex = 1
        TextBoxHost.Text = "Host"
        ' 
        ' TextBoxUsername
        ' 
        TextBoxUsername.Location = New Point(55, 83)
        TextBoxUsername.Name = "TextBoxUsername"
        TextBoxUsername.ReadOnly = True
        TextBoxUsername.Size = New Size(150, 31)
        TextBoxUsername.TabIndex = 2
        TextBoxUsername.Text = "User Name"
        ' 
        ' TextBoxFilePath
        ' 
        TextBoxFilePath.Location = New Point(55, 120)
        TextBoxFilePath.Name = "TextBoxFilePath"
        TextBoxFilePath.ReadOnly = True
        TextBoxFilePath.Size = New Size(150, 31)
        TextBoxFilePath.TabIndex = 3
        TextBoxFilePath.Text = "Path"
        ' 
        ' RichTextBoxPrivateKey
        ' 
        RichTextBoxPrivateKey.Location = New Point(264, 33)
        RichTextBoxPrivateKey.Name = "RichTextBoxPrivateKey"
        RichTextBoxPrivateKey.ReadOnly = True
        RichTextBoxPrivateKey.Size = New Size(467, 321)
        RichTextBoxPrivateKey.TabIndex = 4
        RichTextBoxPrivateKey.Text = "Private Key"
        ' 
        ' LabelCurrentValue
        ' 
        LabelCurrentValue.AutoSize = True
        LabelCurrentValue.Font = New Font("Segoe UI", 14F, FontStyle.Bold Or FontStyle.Italic, GraphicsUnit.Point)
        LabelCurrentValue.Location = New Point(264, 387)
        LabelCurrentValue.Name = "LabelCurrentValue"
        LabelCurrentValue.Size = New Size(105, 38)
        LabelCurrentValue.TabIndex = 5
        LabelCurrentValue.Text = "Label1"
        ' 
        ' ButtonEdit
        ' 
        ButtonEdit.Location = New Point(55, 176)
        ButtonEdit.Name = "ButtonEdit"
        ButtonEdit.Size = New Size(112, 34)
        ButtonEdit.TabIndex = 6
        ButtonEdit.Text = "Edit"
        ButtonEdit.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(ButtonEdit)
        Controls.Add(LabelCurrentValue)
        Controls.Add(RichTextBoxPrivateKey)
        Controls.Add(TextBoxFilePath)
        Controls.Add(TextBoxUsername)
        Controls.Add(TextBoxHost)
        Controls.Add(Button1)
        Name = "Form1"
        Text = "AIOS_DISABLE_LOGIN_WHITELIST"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents TextBoxHost As TextBox
    Friend WithEvents TextBoxUsername As TextBox
    Friend WithEvents TextBoxFilePath As TextBox
    Friend WithEvents RichTextBoxPrivateKey As RichTextBox
    Friend WithEvents LabelCurrentValue As Label
    Friend WithEvents ButtonEdit As Button

End Class
