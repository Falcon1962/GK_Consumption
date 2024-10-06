Imports System.IO
Imports ClosedXML.Excel
Imports Microsoft.Win32


Public Class ExcelTools


    Public Shared Sub GenerateConsumptionReport(_From As String, _To As String, _Data As String)
        ' Create a new workbook
        Dim workbook As New XLWorkbook()

        ' Add a worksheet
        Dim worksheet = workbook.Worksheets.Add("Consumption Report")

        ' Define the report title and data
        Dim s As String = GK.TextLookup("TotalsReport", "Consumption report") & vbCrLf
        s &= Main_Form.TbMachName.Text & vbCrLf
        s &= GK.TextLookup("From", "From: ") & _From & vbCrLf
        s &= GK.TextLookup("To", "To: ") & _To & vbCrLf
        Dim reportTitle As String = s

        Dim rows As String() = _Data.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim ingredientsData As New List(Of List(Of String))

        For Each row As String In rows
            Dim columns As String() = row.Split(New String() {vbTab}, StringSplitOptions.None)
            Dim rowData As New List(Of String)(columns) ' Convert array to List
            ingredientsData.Add(rowData)
        Next

        ' Set report title in cell A1
        worksheet.Cell("A1").Value = reportTitle
        worksheet.Range("A1:E1").Merge().Style.Font.SetBold(True).Font.SetFontSize(14)
        worksheet.Row("1").Height = 100


        ' Fill in the headers & data
        Dim HeaderrowNr As Integer = 2
        For rowIndex As Integer = 0 To ingredientsData.Count - 1
            For colIndex As Integer = 0 To ingredientsData(rowIndex).Count - 1
                worksheet.Cell(rowIndex + HeaderrowNr, colIndex + 1).Value = ingredientsData(rowIndex)(colIndex)
            Next
        Next

        ' Style headers
        Dim HeaderRange As String = $"A{HeaderrowNr}:E{HeaderrowNr}"
        worksheet.Range(HeaderRange).Style.Font.SetBold(True)
        worksheet.Range(HeaderRange).Style.Fill.BackgroundColor = XLColor.LightGray
        worksheet.Range(HeaderRange).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center

        ' Adjust & style column widths
        worksheet.Columns("A:F").AdjustToContents()
        ' Set a minimum width of 12 for columns A to F
        For i As Integer = 1 To 6
            Dim column = worksheet.Column(i)
            If column.Width < 12 Then
                column.Width = 12
            End If
        Next

        worksheet.Range("B3:E33").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right

        ' Add the SRP Logo
        ' Convert the PNG image from resources to a memory stream
        Using memoryStream As New MemoryStream()
            ' Access the image from resources and save it to the memory stream
            My.Resources.Logo_SRP.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png)

            ' Reset the stream position to the beginning before reading
            memoryStream.Position = 0


            ' Add the image to the worksheet
            Dim columnWidth As Double = worksheet.Column(5).Width * 7  ' Width in pixels
            Dim PictureWidth As Integer = 200
            Dim PictureHeight As Integer = 100
            Dim xOffset As Integer = CInt(columnWidth - PictureWidth)
            Dim picture = worksheet.AddPicture(memoryStream)
            With picture
                .MoveTo(worksheet.Cell("E1"), xOffset, 0)  ' Set the cell where the image will be placed
                .WithSize(PictureWidth, PictureHeight)           ' Optional: Set the size of the image
            End With
        End Using


        ' Save the file
        Dim filePath As String = "ConsumptionReport.xlsx"
        workbook.SaveAs(filePath)

        If IsExcelInstalled() Then
            Dim processStartInfo As New ProcessStartInfo
            processStartInfo.FileName = filePath
            processStartInfo.UseShellExecute = True
            Process.Start(processStartInfo)
        Else
            MessageBox.Show($"Excel report generated: {filePath}", "Excel not installed")
        End If

    End Sub




    Private Shared Function IsExcelInstalled2() As Boolean
        Dim excelKeyPath As String = "SOFTWARE\Microsoft\Office\"
        Dim officeVersions As String() = {"16.0", "15.0", "14.0", "12.0"} ' Excel 2016, 2013, 2010, 2007

        For Each version As String In officeVersions
            Dim excelRegKey As String = excelKeyPath & version & "\Excel\InstallRoot"

            ' Check both the standard and the WOW6432Node path for 32-bit Excel on 64-bit systems
            Dim installPath As String = GetExcelInstallPath(excelRegKey)
            If String.IsNullOrEmpty(installPath) Then
                installPath = GetExcelInstallPath("SOFTWARE\WOW6432Node\" & excelRegKey)
            End If

            If Not String.IsNullOrEmpty(installPath) Then
                ' Excel is installed if InstallRoot path contains "Path" key
                MessageBox.Show(installPath)
                Return True
            End If
        Next

        ' Excel not found
        Return False
    End Function

    Private Shared Function IsExcelInstalled() As Boolean
        Dim s As String
        Try
            'Check if Excel is available in the correct version
            Dim regKey As RegistryKey
            regKey = My.Computer.Registry.ClassesRoot.OpenSubKey("Excel.Application", False).OpenSubKey("CurVer", False)
            s = regKey.GetValue("").ToString
        Catch ex As Exception
            s = ""
        End Try
        If s.Length > 0 Then
            Dim RegKeys() As String = s.Split(".")
            Dim Excelversion As Integer = CInt(RegKeys(RegKeys.Count - 1))
            Return (Excelversion >= 14)
        Else
            Return False
        End If
    End Function







    Private Shared Function GetExcelInstallPath(ByVal registryKey As String) As String
        Dim regKey As RegistryKey = Registry.LocalMachine.OpenSubKey(registryKey)
        If regKey IsNot Nothing Then
            Dim installPath As Object = regKey.GetValue("Path")
            If installPath IsNot Nothing Then
                Return installPath.ToString()
            End If
        End If
        Return Nothing
    End Function


End Class
