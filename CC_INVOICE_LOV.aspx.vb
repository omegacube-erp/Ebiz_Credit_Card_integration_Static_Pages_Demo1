Partial Class _CC_INVOICE_LOV
 Inherits System.Web.UI.Page 
Public d, d1 As Int32
Public ds1, ds2, ds3, ds4, ds5 As New Data.DataSet()
Public sxml, vn, vn1, vn2 As String
Public primary1, primary2, primary3, primary4,primary5,primary6 As String
Public dfetch As New Data.DataSet()
Public sc, scn, scn2 As String
Protected dbad As New OleDbDataAdapter
 Public dbadc As New OleDbCommand
Public newBox As New MessageBox
       public conn As New Dbconn
Public param1, param2, param3, param4,param5,param6 As String
Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim conn As New Dbconn

Row_count_message.Visible=False
        dbad.SelectCommand = New OleDbCommand
        dbad.SelectCommand.Connection = conn.getconnection()
param1=Request.QueryString.Get("param1")
param2=Request.QueryString.Get("param2")
param3=Request.QueryString.Get("param3")
param4=Request.QueryString.Get("param4")
param5=Request.QueryString.Get("param5")
param6=Request.QueryString.Get("param6")
Dim sval as string=""
        If Not IsPostBack Then
            If (Session("client") = "") Then
                Session("client") = "TEST"
            End If
 TxtFilterName1.Focus()
                Session("TRANREFNUM") = ""
                Session("INVOICE_NO") = ""
                Session("AUTHCODE") = ""
                Session("AUTH_TRANS_DATE") = ""
                Session("MASKEDCC") = ""
                Session("Where") = ""
 If (sval <> "") Then
                TxtFilterName1.Text = sval
                Call keyword_go_Click(sender, e)
            Else
                Call data_bind()
            End If
        End If
    End Sub
Public sub Create_where_condition()
Dim s6, s7 as string
Dim db_s_dbg as new data.Dataset
s6=""
s7=""
 if (Session("TRANREFNUM") <> "") Then
s6= s6 & "TRANREFNUM='" & Session("TRANREFNUM") & "'  and "
s7=s7 & " Select 'TRANREFNUM' s_field,'Ref No' s_field_title, '" & Session("TRANREFNUM") & "' s_field_value from dual union all "
End If
 if (Session("INVOICE_NO") <> "") Then
s6= s6 & "INVOICE_NO='" & Session("INVOICE_NO") & "'  and "
s7=s7 & " Select 'INVOICE_NO' s_field,'Invoice No' s_field_title, '" & Session("INVOICE_NO") & "' s_field_value from dual union all "
End If
 if (Session("AUTHCODE") <> "") Then
s6= s6 & "AUTHCODE='" & Session("AUTHCODE") & "'  and "
s7=s7 & " Select 'AUTHCODE' s_field,'Auth Code' s_field_title, '" & Session("AUTHCODE") & "' s_field_value from dual union all "
End If
 if (Session("AUTH_TRANS_DATE") <> "") Then
s6= s6 & "AUTH_TRANS_DATE='" & Session("AUTH_TRANS_DATE") & "'  and "
s7=s7 & " Select 'AUTH_TRANS_DATE' s_field,'Date' s_field_title, '" & Session("AUTH_TRANS_DATE") & "' s_field_value from dual union all "
End If
 if (Session("MASKEDCC") <> "") Then
s6= s6 & "MASKEDCC='" & Session("MASKEDCC") & "'  and "
s7=s7 & " Select 'MASKEDCC' s_field,'CC' s_field_title, '" & Session("MASKEDCC") & "' s_field_value from dual union all "
End If
If (Len(s6) > 5) Then
s6 = Mid(s6, 1, Len(s6) - 4)
                Session("Where") ="WHERE CUSTOMER_NO='" & PARAM1 & "' and " &  s6
Else
                Session("Where") = ""
End If
If (Len(s7) > 10) Then
s7 = Mid(s7, 1, Len(s7) - 10)
            dbad.SelectCommand.CommandText = s7
            db_s_dbg.Clear()
            dbad.Fill(db_s_dbg)
            If (db_s_dbg.Tables(0).Rows.Count > 0) Then
PanelLOV_s.Visible=True
                search_dbg.DataSource = db_s_dbg.Tables(0)
                search_dbg.DataBind()
Else
PanelLOV_s.Visible=False
            End If
Else
PanelLOV_s.Visible=False
End If
    End Sub
Public Sub checkbox0_change_s(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim dgItem1 As DataGridItem
        Dim p
        Dim dds As New Data.DataSet
            Dim cell4 As TableCell
        Dim cb As New checkbox
        Dim lno, st1 As String
        cb = CType(sender, checkbox)
        cell4 = CType(cb.Parent, TableCell)
        dgItem1 = CType(cell4.Parent, DataGridItem)
        Dim s_field As Label = CType(dgItem1.FindControl("s_field"), Label)
        session("" & s_field.Text & "")=""
	call Create_where_condition()
  If (Session("Where") <> "") Then
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO " &  Session("Where") & " ORDER BY  AUTH_TRANS_DATE desc"
            ds4.Clear()
            dbad.Fill(ds4)
                db_cust.DataSource = ds4.Tables(0)
                db_cust.DataBind()
Else
Call data_bind()
End If
End Sub
Protected Sub Clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clear.Click
                Session("TRANREFNUM") = ""
                Session("INVOICE_NO") = ""
                Session("AUTHCODE") = ""
                Session("AUTH_TRANS_DATE") = ""
                Session("MASKEDCC") = ""
Session("Where")=""
TxtFilterName1.Text=""
field_search_k.SelectedIndex = 0 
Field_search_txt.Text = ""
	call Create_where_condition()
	        call data_bind()
    End Sub
Public Sub data_bind()
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO WHERE CUSTOMER_NO='" & PARAM1 & "' ORDER BY  AUTH_TRANS_DATE desc"
            ds4.Clear()
            dbad.Fill(ds4)
                db_cust.DataSource = ds4.Tables(0)
                db_cust.DataBind()
End Sub
	Protected Sub db_cust_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles db_cust.PageIndexChanged
If (field_search_k.SelectedIndex > 0 And Field_search_txt.Text <> "") Then
    Call Keyword_search_ddl(field_search_k.SelectedItem.Value, Field_search_txt.Text, e.NewPageIndex)
Else
    Call Keyword_search(e.NewPageIndex)
End If
	End Sub
Protected Sub keyword_go_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles keyword_go.Click
Call Keyword_search(0)
End Sub
 Protected Sub field_serch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles field_serch.Click
        If (field_search_k.SelectedIndex > 0 And Field_search_txt.Text <> "") Then
            Call Keyword_search_ddl(field_search_k.SelectedItem.Value, Field_search_txt.Text,0)
        End If
    End Sub
Public Sub Keyword_search(ByVal page_index As Integer)
        Dim s As String
db_cust.CurrentPageIndex = page_index
        If (TxtFilterName1.Text <> "") Then
            s = Replace(TxtFilterName1.Text, "'", "''")
     if (Session("Where") = "") Then
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO WHERE CUSTOMER_NO='" & PARAM1 & "' and ( (upper(TRANREFNUM) like upper('%" & s & "%') ) or (upper(INVOICE_NO) like upper('%" & s & "%') ) or (upper(AUTHCODE) like upper('%" & s & "%') ) or (upper(MASKEDCC) like upper('%" & s & "%') ) ) ORDER BY  AUTH_TRANS_DATE desc"
Else
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO " &  Session("Where") & "  and ( (upper(TRANREFNUM) like upper('%" & s & "%') ) or (upper(INVOICE_NO) like upper('%" & s & "%') ) or (upper(AUTHCODE) like upper('%" & s & "%') ) or (upper(MASKEDCC) like upper('%" & s & "%') ) ) ORDER BY  AUTH_TRANS_DATE desc"
End If
            ds4.Clear()
            dbad.Fill(ds4)
                db_cust.DataSource = ds4.Tables(0)
                db_cust.DataBind()
        Else
     if (Session("Where") = "") Then
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO WHERE CUSTOMER_NO='" & PARAM1 & "' ORDER BY  AUTH_TRANS_DATE desc"
Else
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO " &  Session("Where") & " ORDER BY  AUTH_TRANS_DATE desc"
End If
            ds4.Clear()
            dbad.Fill(ds4)
                db_cust.DataSource = ds4.Tables(0)
                db_cust.DataBind()
        End If
End Sub
Public Sub Keyword_search_ddl(ByVal fname As String, ByVal fvalue As String, ByVal page_index As Integer)
        Dim s As String
db_cust.CurrentPageIndex = page_index
        If (fvalue <> "" and fname<>"") Then
                Session("TRANREFNUM") = ""
                Session("INVOICE_NO") = ""
                Session("AUTHCODE") = ""
                Session("AUTH_TRANS_DATE") = ""
                Session("MASKEDCC") = ""
Session("Where")=""
TxtFilterName1.Text=""
	call Create_where_condition()
            s = Replace(fvalue, "'", "''")
            dbad.SelectCommand.CommandText = "SELECT INVOICE_NO,AUTHCODE,AUTH_TRANS_DATE,TRANREFNUM,MASKEDCC FROM V_CC_INVOICE_PAYMENT_INFO WHERE CUSTOMER_NO='" & PARAM1 & "' and (upper(" & fname & ") like upper('%" & s & "%') ) ORDER BY  AUTH_TRANS_DATE desc"
            ds4.Clear()
            dbad.Fill(ds4)
                db_cust.DataSource = ds4.Tables(0)
                db_cust.DataBind()
        End If
End Sub
Protected Sub db_cust_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles db_cust.ItemDataBound
        Dim tblStoreName As TableRow
        Dim n As String
	Select Case e.Item.ItemType
            Case ListItemType.Item
                n = e.Item.Cells(0).Text
                tblStoreName = e.Item.FindControl("L_TRANREFNUM").Parent.Parent
if (Session("HIDE_MENU_OPTIONS") = "Y") Then
                tblStoreName.Attributes.Add("OnClick", "ShowDetails1('" & Request.QueryString.Get("RETURN_ID") & "','" & n & "');")
Else
if (Session("BROWSER_OPTIONS") = "Y") Then
                tblStoreName.Attributes.Add("OnDblClick", "ShowDetails1('" & Request.QueryString.Get("RETURN_ID") & "','" & n & "');")
Else
                tblStoreName.Attributes.Add("OnDblClick", "ShowDetails('" & n & "');")
End If
End If
                tblStoreName.Attributes.Add("onmouseover", "this.className='rlDataRowStyleA'")
                tblStoreName.Attributes.Add("onmouseout", "this.className='rlDataRowStyleB'")
            Case ListItemType.AlternatingItem
                n = e.Item.Cells(0).Text
                tblStoreName = e.Item.FindControl("L_INVOICE_NO").Parent.Parent
if (Session("HIDE_MENU_OPTIONS") = "Y") Then
                tblStoreName.Attributes.Add("OnClick", "ShowDetails1('" & Request.QueryString.Get("RETURN_ID") & "','" & n & "');")
Else
if (Session("BROWSER_OPTIONS") = "Y") Then
                tblStoreName.Attributes.Add("OnDblClick", "ShowDetails1('" & Request.QueryString.Get("RETURN_ID") & "','" & n & "');")
Else
                tblStoreName.Attributes.Add("OnDblClick", "ShowDetails('" & n & "');")
End If
End If
                tblStoreName.Attributes.Add("onmouseover", "this.className='rlDataRowStyleA'")
                tblStoreName.Attributes.Add("onmouseout", "this.className='rlDataRowStyleD'")
            Case ListItemType.Header
Dim ifilter As Integer
call Create_where_condition()
        End Select
End Sub
Public Shared Function Wrap(ByVal text As String, ByVal maxLength As Integer) As String
        text = text.Replace(vbLf, " ")
        text = text.Replace(vbCr, " ")
        text = text.Replace(".", ". ")
        text = text.Replace(">", "> ")
        text = text.Replace(vbTab, " ")
        text = text.Replace(",", ", ")
        text = text.Replace(";", "; ")
        Dim Words As String() = text.Split(" "c)
        Dim currentLineLength As Integer = 0
        Dim Lines As New ArrayList(text.Length / maxLength)
        Dim currentLine As String = ""
        Dim InTag As Boolean = False
        For Each currentWord As String In Words
            If currentWord.Length > 0 Then
                If currentWord.Substring(0, 1) = "<" Then
                    InTag = True
                End If
                If InTag Then
                    If currentLine.EndsWith(".") Then
                        currentLine += currentWord
                    Else
                        currentLine += " " & currentWord
                    End If
                    If currentWord.IndexOf(">") > -1 Then
                        InTag = False
                    End If
                Else
                    If currentLineLength + currentWord.Length + 1 < maxLength Then
                        currentLine += " " & currentWord
                        currentLineLength += (currentWord.Length + 1)
                    Else
                        Lines.Add(currentLine)
                        currentLine = currentWord
                        currentLineLength = currentWord.Length
                    End If
                End If
            End If
        Next
        If currentLine <> "" Then
            Lines.Add(currentLine)
        End If
        Dim textLinesStr As String() = New String(Lines.Count - 1) {}
        Lines.CopyTo(textLinesStr, 0)
        Dim str1 As String
        str1 = ""
        Dim i As Integer
        For i = 0 To textLinesStr.Length - 1
            str1 = str1 & textLinesStr(i) & "<br />"
        Next
        Return str1
    End Function
    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        dbad.SelectCommand.Connection.Close()
        dbad.Dispose()
    End Sub
End Class
