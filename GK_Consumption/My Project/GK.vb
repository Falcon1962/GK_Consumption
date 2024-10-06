Public Class GK

    Public Shared LanguageText As String


    Public Shared Function TextLookup(ByVal LookupText As String, Optional ByVal AlternativeText As String = "") As String
        Dim Reply As String
        Dim s As String
        Dim pos As Integer = 0
        Dim Found As Boolean = False

        Dim TotalText = LanguageText

        Reply = Environment.NewLine & TotalText

        pos = InStr(TotalText, Environment.NewLine & LookupText)
        If pos = 0 Then
            If (AlternativeText = "") Then
                Reply = "_" & LookupText
            Else
                Reply = AlternativeText
            End If
        Else
            While (Not Found) 'Or (pos = 0)
                pos = InStr(Reply, LookupText)
                If pos > 0 Then
                    'Remove everything to pos
                    Reply = Mid(Reply, pos)
                    'find "="
                    pos = InStr(Reply, "=")
                    s = Mid(Reply, Len(LookupText) + 1, pos - Len(LookupText) - 1)
                    If Trim(s) = "" Then
                        Found = True
                    Else
                        Reply = Mid(Reply, pos)
                    End If
                Else
                    Exit While
                End If
            End While
            If Found Then
                'Remove everything to past "="
                Reply = Mid(Reply, pos + 1)
                pos = InStr(Reply, Environment.NewLine)
                'Only current line
                Reply = Trim(Mid(Reply, 1, pos - 1))
                Reply = Replace(Reply, "/'", "/`")
                pos = InStr(Reply, "'")
                'Remove comment
                If pos > 0 Then
                    Reply = Mid(Reply, 1, pos - 1)
                End If
                Reply = Replace(Reply, "/`", "'")
            Else
                If (AlternativeText = "") Then
                    Reply = "_" & LookupText
                Else
                    Reply = AlternativeText
                End If
            End If
        End If
        Reply = Replace(Reply, "//", Environment.NewLine)

        Return Reply
    End Function

End Class
