<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="example.aspx.vb" Inherits="Test_API_WEB.example" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label8" runat="server" Text="" ForeColor="Red" Visible="false"></asp:Label>
        <table>
            <tr> <td colspan="2">欄位名稱說明:</td></tr>
            <tr>
                <td>id</td><td>編號</td>
            </tr>
            <tr>
                <td>Name</td><td>姓名</td>
            </tr>
             <tr>
                <td>Score</td><td>分數</td>
            </tr>
            <tr>
                <td>Test_Date</td><td>考試日期</td>
            </tr>
             <tr>
                <td>Test_Time</td><td>考試時間</td>
            </tr>
            </table>
        <asp:Button ID="Button2" runat="server" Text="自動取得" OnClick="Button2_Click" />
        <table style="border:1px; ">
            <tr>
               <td> <asp:Label ID="Label7" runat="server" Text="傳送方式"></asp:Label>
                <asp:DropDownList ID="Type" runat="server"><asp:ListItem Value="POST">POST</asp:ListItem><asp:ListItem Value="GET">GET</asp:ListItem></asp:DropDownList>
            </td>
                  </tr>
            <tr>
                <td>
        <asp:Label ID="Label5" runat="server" Text="帳號"></asp:Label>  
        <asp:TextBox ID="account" runat="server" Text=""></asp:TextBox>
         <asp:Label ID="Label6" runat="server" Text="密碼" ></asp:Label>  
        <asp:TextBox ID="passwd" runat="server" Text=""></asp:TextBox>
                    </td>
             </tr>
            <tr>
                <td>
        <asp:Label ID="Label1" runat="server" Text="序號"></asp:Label>  
        <asp:TextBox ID="Nb" runat="server" Text=""></asp:TextBox>
         <asp:Label ID="Label9" runat="server" Text="身分證字號或手機的唯一碼" ></asp:Label>  
        <asp:TextBox ID="IDNO" runat="server" Text=""></asp:TextBox>
                    </td>
             </tr>
            <tr>
                 <td>
        <asp:Label ID="Label4" runat="server" Text="顯示的欄位" ></asp:Label>  
        <asp:TextBox ID="filed" runat="server" Text="Name,id,ExamDate,IDNO"  Width="250"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                <td>
        <asp:Label ID="Label2" runat="server" Text="區間"></asp:Label>  
                <input type="text" id="st_date" runat="server" value="2023-08-01"/> <asp:Label ID="Label3" runat="server" Text="~"></asp:Label>
                      <input type="text" id="end_date" runat="server" value="2023-08-01"/>
                    </td>
                   <asp:Button ID="Button1" runat="server" Text="取得資料"  OnClick="Button1_Click"/>
                </tr>
            </table>
             <div id="status" runat="server">

            </div>
          <asp:GridView ID="gvAuth" runat="server" AutoGenerateColumns="false" BorderWidth="2px" CellPadding="2" Width="60%">
                            <Columns>
                                <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="姓名">
                                    <ItemTemplate>
                                        <asp:Label ID="std_name" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#6C3411" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="代號">
                                    <ItemTemplate>
                                        <asp:Label ID="id" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#6C3411" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="日期">
                                    <ItemTemplate>
                                        <asp:Label ID="ExamDate" runat="server" Text='<%# Eval("ExamDate")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#6C3411" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="white" HeaderText="身分">
                                    <ItemTemplate>
                                        <asp:Label ID="IDNO" runat="server" Text='<%# Eval("IDNO")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#6C3411" />
                                </asp:TemplateField>
                               
                                </Columns>
              </asp:GridView>
    </form>
</body>
</html>
