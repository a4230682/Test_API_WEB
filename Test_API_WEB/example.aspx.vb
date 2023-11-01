Imports System.Net
Imports Newtonsoft.Json
Imports System.IO
Imports System.Security.Cryptography

Public Class PostData
    Public Field As String
    Public S_day As String
    Public E_day As String
End Class
Public Class example
    Inherits System.Web.UI.Page
    Dim domain As String = "api.tilc.com.tw"
    Private Function SurroundingSub() As String
        Dim Url As String = "http://" + domain + "/Exam"
        Dim result As String
        Dim res As WebClient = New WebClient
        res.Encoding = Encoding.UTF8
        res.Headers.Add(HttpRequestHeader.Accept, "application/xml")
        res.Headers.Add(HttpRequestHeader.ContentType, "application/json")
        res.Headers.Add("T-TOEIC-Account", account.Text)
        '如要查詢特定的人，需要再加入以下條件
        If Nb.Text <> "" Then
            res.Headers.Add("T-TOEIC-ID", Nb.Text)
        End If
        If IDNO.Text <> "" Then
            res.Headers.Add("T-TOEIC-IDNO", IDNO.Text)
        End If
        Dim postData As PostData = New PostData() With {
                    .Field = filed.Text.Trim,
                    .S_day = st_date.Value,
                    .E_day = end_date.Value
                }
        Dim json As String = JsonConvert.SerializeObject(postData)
        Dim Key As String = passwd.Text + Url + json
        res.Headers.Add("T-TOEIC-Authorization", Encryption(Key, passwd.Text))
        result = res.UploadString(Url, json)
        Return result
    End Function
    Public Function Encryption(Json As String, key As String) As String
        Dim message As String = Json
        Dim secretKey As String = key
        ' Convert the message and secret key to bytes
        Dim messageBytes As Byte() = Encoding.UTF8.GetBytes(message)
        Dim secretKeyBytes As Byte() = Encoding.UTF8.GetBytes(secretKey)
        ' Create an HMACSHA256 instance with the secret key
        Using hmacsha256 As New HMACSHA256(secretKeyBytes)
            ' Compute the hash
            Dim hashBytes As Byte() = hmacsha256.ComputeHash(messageBytes)
            ' Convert the hash to a hexadecimal string
            Dim hmac As String = BitConverter.ToString(hashBytes).Replace("-", "").ToLower()
            '  Console.WriteLine("HMAC: " & hmac)
            Return hmac
        End Using
    End Function
    Public Sub Auto_Post_Data()
        Dim Year As Integer = 2023
        Dim month As Integer = 1
        Dim day As Integer = 31
        Dim Count As Integer = 0
        Dim start_time As DateTime
        Dim end_time As DateTime
        Dim All_Time As TimeSpan
        Dim end_day As String
        For i As Integer = month To 12
            start_time = Now
            Count = Count + 1
            Response.Write("第" & Count & "測試,開始時間:＂ & start_time.ToString("yyyy年MM月dd HH點mm分ss秒<br>"))
            If i < 10 Then
                st_date.Value = Year & "-0" & i & "-01"
                If i = 1 Or i = 3 Or i = 5 Or i = 7 Or i = 8 Or i = 11 Then
                    end_date.Value = Year & "-0" & i & "-31"
                ElseIf i = 2 Then
                    end_date.Value = Year & "-0" & i & "-28"
                Else
                    end_date.Value = Year & "-0" & i & "-30"
                End If
            Else
                st_date.Value = Year & "-" & i & "-01"
                If i = 10 Or i = 12 Then
                    end_date.Value = Year & "-" & i & "-31"
                Else
                    end_date.Value = Year & "-" & i & "-30"
                End If
            End If

            Response.Write("查詢區間:" & st_date.Value & "~" & end_date.Value & "<br>")
            Response.Write("取得筆數:" & Get_Data_Length() & "筆<br>")
            end_time = Now
            All_Time = end_time - start_time

            Response.Write("第" & Count & "測試,結束時間:＂ & end_time.ToString("yyyy年MM月dd HH點mm分ss秒") & "<br> 總共花費時間:" & All_Time.ToString & "<br>")

        Next

    End Sub
    Public Function Get_Data_Length() As Integer
        Dim Dt As DataSet = New DataSet()
        Dim tclient As WebClient = New WebClient()
        Dim res As String
        Try
            res = WebUtility.HtmlDecode(SurroundingSub())
            Dim re As StringReader = New StringReader(res)
            Dt.ReadXml(re)
            Dim T_Html As String = "<table><tr>"
            Dim T_Header As String = ""
            If Dt.Tables.Count > 2 Then
                For k As Integer = 0 To Dt.Tables("Item").Columns.Count - 1
                    T_Header += "<th>" + Dt.Tables("Item").Columns(k).ColumnName + "</th>"
                Next
                T_Html += T_Header + "</tr>"
                For i As Integer = 0 To Dt.Tables("Item").Rows.Count - 1
                    T_Html += "<tr>"
                    T_Html += "<td>" & Dt.Tables("Item").Rows(i).Item(0) & "</td>"
                    T_Html += "<td>" & Dt.Tables("Item").Rows(i).Item(1) & "</td>"
                    T_Html += "<td>" & Dt.Tables("Item").Rows(i).Item(2) & "</td>"
                    T_Html += "<td>" & Dt.Tables("Item").Rows(i).Item(3) & "</td>"
                    T_Html += "</tr>"
                Next
                T_Html += "</table><hr>"
                Response.Write(T_Html)
                Return Dt.Tables("Item").Rows.Count
            End If
        Catch ex As Exception
            Label8.Text = ex.Message + end_date.Value + "," + st_date.Value
            Label8.Visible = True
            Exit Function
            'Set_View_Nothing()

        End Try
        Return 0
    End Function
    Public Sub Get_Data()
        Dim Dt As DataSet = New DataSet()
        Dim tclient As WebClient = New WebClient()
        Dim res As String
        Try
            res = WebUtility.HtmlDecode(SurroundingSub())
            Dim re As StringReader = New StringReader(res)
            Dt.ReadXml(re)
            status.InnerHtml = "<h4>取得資料狀態:</h4>"
            status.InnerHtml += "  <table>
                                    <tr>
                                <th>狀態碼</th><td><label>" + Dt.Tables(0).Rows(0).Item("Status") + "</label></td>
                                 <th>訊息</th><td><label>" + Dt.Tables(0).Rows(0).Item("Message") + "</label></td>
                                  </tr>
                                 </table><br>"
            If Dt.Tables.Count >= 1 Then
                gvAuth.DataSource = Dt.Tables("Item")
                gvAuth.DataBind()
            Else
                Set_View_Nothing()
            End If
        Catch ex As Exception
            Label8.Text = ex.Message + end_date.Value + "," + st_date.Value
            Label8.Visible = True
            Set_View_Nothing()
        End Try
    End Sub
    Public Sub Set_View_Nothing()
        gvAuth.DataSource = Nothing
        gvAuth.DataBind()
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        Label8.Visible = False
        Get_Data()
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Auto_Post_Data()
    End Sub
End Class