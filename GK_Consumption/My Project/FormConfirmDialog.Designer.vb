<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormConfirmDialog
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        lblMessage = New Label()
        btnYes = New Button()
        btnNo = New Button()
        SuspendLayout()
        ' 
        ' lblMessage
        ' 
        lblMessage.AutoSize = True
        lblMessage.Location = New Point(36, 35)
        lblMessage.Name = "lblMessage"
        lblMessage.Size = New Size(78, 15)
        lblMessage.TabIndex = 0
        lblMessage.Text = "Are you sure?"
        ' 
        ' btnYes
        ' 
        btnYes.Location = New Point(39, 71)
        btnYes.Name = "btnYes"
        btnYes.Size = New Size(75, 50)
        btnYes.TabIndex = 1
        btnYes.Text = "Yes"
        btnYes.UseVisualStyleBackColor = True
        ' 
        ' btnNo
        ' 
        btnNo.Location = New Point(196, 71)
        btnNo.Name = "btnNo"
        btnNo.Size = New Size(75, 50)
        btnNo.TabIndex = 2
        btnNo.Text = "No"
        btnNo.UseVisualStyleBackColor = True
        ' 
        ' FormConfirmDialog
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(320, 132)
        Controls.Add(btnNo)
        Controls.Add(btnYes)
        Controls.Add(lblMessage)
        Name = "FormConfirmDialog"
        Text = "Confirm Yes/No"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblMessage As Label
    Friend WithEvents btnYes As Button
    Friend WithEvents btnNo As Button
End Class
