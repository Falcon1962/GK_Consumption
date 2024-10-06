Public Class FormConfirmDialog
    Public Property Question As String


    Private Sub FormConfirmDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            ' Set the label to display the question passed from the main form
            lblMessage.Text = Question
        End Sub

        ' When the Yes button is clicked, return DialogResult.Yes
        Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
            Me.DialogResult = DialogResult.Yes
            Me.Close()
        End Sub

        ' When the No button is clicked, return DialogResult.No
        Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
            Me.DialogResult = DialogResult.No
            Me.Close()
        End Sub
    End Class

