Imports System.IO
Imports System.Net
Imports System.Xml
Partial Class eBiz_Credit_Auth_Returns
    Inherits System.Web.UI.Page
    Protected dbad As New OleDbDataAdapter
    Public conn As New Dbconn
    Public Invoice_no, ORDER_NO, line_no, resp, ptype As String
    Public TLurl, TLactionURL, TLPassword, TLUserId, SECURITY_ID, CLIENT_URL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dbad.SelectCommand = New OleDbCommand
        dbad.InsertCommand = New OleDbCommand
        dbad.UpdateCommand = New OleDbCommand
        dbad.DeleteCommand = New OleDbCommand
        If (Session("user_id") = "") Then
            Response.Redirect("LOGIN.ASPX")
        End If
        If Not IsPostBack Then

            If (Request.QueryString("ORDER_NO") <> "") Then

                ORDER_NO = Request.QueryString.Get("ORDER_NO")
                'added Code for getting URL and Action Id from TOOL Settings on 27 AUGUST 2020 by deepu
                'getting url from Tool Settings
                Try
                    Dim dsurl As New Data.DataSet
                    dsurl = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_URL'")
                    dsurl.Clear()
                    dbad.Fill(dsurl)
                    If (dsurl.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsurl.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            TLurl = dsurl.Tables(0).Rows(0)(0)
                        Else
                            TLurl = ""
                        End If
                    End If
                Catch ex As Exception
                    TLurl = "https://soap.ebizcharge.net/eBizService.svc"
                End Try

                'getting actionurl from Tool Settings
                Try
                    Dim dsactionurl As New Data.DataSet
                    dsactionurl = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_ACTION_URL_RETURNS'")
                    dsactionurl.Clear()
                    dbad.Fill(dsactionurl)
                    If (dsactionurl.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsactionurl.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            TLactionURL = dsactionurl.Tables(0).Rows(0)(0)
                        Else
                            TLactionURL = ""
                        End If
                    End If
                Catch ex As Exception
                    TLactionURL = "http://eBizCharge.ServiceModel.SOAP/IeBizService/runTransaction"
                End Try

                'getting userid from Tool Settings
                Try
                    Dim dsuserid As New Data.DataSet
                    dsuserid = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_USER_ID'")
                    dsuserid.Clear()
                    dbad.Fill(dsuserid)
                    If (dsuserid.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsuserid.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            TLUserId = dsuserid.Tables(0).Rows(0)(0)
                        Else
                            TLUserId = ""
                        End If
                    End If
                Catch ex As Exception
                    TLUserId = "omegacube"
                End Try
                'getting password from Tool Settings
                Try
                    Dim dspassword As New Data.DataSet
                    dspassword = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_PASSWORD'")
                    dspassword.Clear()
                    dbad.Fill(dspassword)
                    If (dspassword.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dspassword.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            TLPassword = dspassword.Tables(0).Rows(0)(0)
                        Else
                            TLPassword = ""
                        End If
                    End If
                Catch ex As Exception
                    TLPassword = "omegacube"
                End Try



                'getting Security from Tool Settings
                Try
                    Dim dssecurity As New Data.DataSet
                    dssecurity = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_SECURITY_ID'")
                    dssecurity.Clear()
                    dbad.Fill(dssecurity)
                    If (dssecurity.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dssecurity.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            SECURITY_ID = dssecurity.Tables(0).Rows(0)(0)
                        Else
                            SECURITY_ID = ""
                        End If
                    End If
                Catch ex As Exception
                    SECURITY_ID = "8303a4ef-025c-439a-97f7-6b79bc2aff16"
                End Try


                'getting Client URL from Tool Settings
                'Try
                'Dim dsclienturl As New Data.DataSet
                'dsclienturl=Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_CLIENT_URL'")
                'dsclienturl.Clear()
                'dbad.Fill(dsclienturl)
                'If(dsclienturl.Tables(0).Rows.Count>0)Then
                'If Not (Equals(dsclienturl.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                'CLIENT_URL=dsclienturl.Tables(0).Rows(0)(0)
                'Else
                'CLIENT_URL=""
                'End If
                'End If
                'Catch ex As Exception
                'CLIENT_URL = "https://erpdoublestarusa.com:8443/"
                'End Try
                'ended Code for getting URL and Action Id from TOOL Settings on 27 AUGUST 2020 by deepu
                Dim ds As New Data.DataSet
                ' ds = Return_record_set("SELECT A.REFERENCE_NO,SUM(B.LINE_TOTAL_AMOUNT) AMOUNT FROM SD_CREDIT_MEMO_HEADER A,SD_CREDIT_MEMO_DETAIL B,SD_INVOICE_CC_PAYMENT C WHERE nvl(a.CREDIT_MEMO_CONFIRMATION,'N')='N' AND A.CREDIT_MEMO_NO=B.CREDIT_MEMO_NO AND A.REFERENCE_NO=C.TRANREFNUM AND Pkg_status.sf_check_status( 'SD_CREDIT_MEMO_HEADER', A.CREDIT_MEMO_NO,NULL,NULL,NULL,NULL,NULL)='Open' AND A.CREDIT_MEMO_NO='" & CREDIT_MEMO_NO & "'  GROUP BY A.REFERENCE_NO")


                ds = Return_record_set("SELECT ORDER_NO,LINE_NO,INVOICE_NO,round(AMOUNT,2) AMOUNT,PAYMENT_RESPONSE,AUTHCODE,AUTH_TRANS_USER,AUTH_TRANS_ID,AUTH_TRANS_DATE,API_TOKEN,API_TOKEN_SESSION,TRANREFNUM,PAYBYTYPE,CCTYPE,MASKEDCC,PMTOKEN,CUSTTOKEN,TRANRESULTCODE,CHANGED_BY,CHANGED_DATE,CREATED_BY,CREATED_DATE FROM SD_ORDER_CC_PAYMENT Where ORDER_NO='" & Request.QueryString.Get("ORDER_NO") & "'")
                If ds.Tables(0).Rows.Count > 0 Then
                    'Dim str1 As String = SOAPManual(ds.Tables(0), Session("USER_ID"))
                    'added on 25 AUGUST 2020


                    Invoice_no = Request.QueryString.Get("INVOICE_NO")
                    Dim dsTransacId As New Data.DataSet
                    dsTransacId = Return_record_set("select * from SD_INVOICE_CC_PAYMENT where INVOICE_NO='" & Invoice_no & "' AND STATUS ='Approved'")
                    dsTransacId.Clear()
                    dbad.Fill(dsTransacId)
                    If (dsTransacId.Tables(0).Rows.Count > 0) Then
                        ' Insert_query("INSERT INTO SD_INVOICE_CC_PAYMENT (INVOICE_NO,LINE_NO,AMOUNT,PAYMENT_TYPE,API_TOKEN,CREATED_BY,CREATED_DATE) select INVOICE_NO,1,balance_amount,'" & ptype & "','','" & Session("USER_ID") & "',SYSDATE FROM V_BALANCE_INVOICE_TO_RECEIVE WHERE NVL(balance_amount,0)>0 AND INVOICE_NO='" & Invoice_no & "'")
                        Dim strFile1 As String = "TEST"
                        Dim strCmd1 As String
                        strCmd1 = String.Format("alert('This invoice already Processed.');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                        ' strCmd1 = String.Format("window.close();", strFile1)
                        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                        Exit Sub
                    Else
                        Delete_query("DELETE FROM SD_INVOICE_CC_PAYMENT where INVOICE_NO='" & Invoice_no & "'")
                        Insert_query("INSERT INTO SD_INVOICE_CC_PAYMENT (INVOICE_NO,LINE_NO,AMOUNT,PAYMENT_TYPE,API_TOKEN,CREATED_BY,CREATED_DATE,TRANREFNUM,AUTHCODE) select INVOICE_NO,1,balance_amount,'" & ptype & "','','" & Session("USER_ID") & "',SYSDATE,'" & ds.tables(0).rows(0)("TRANREFNUM") & "','" & ds.tables(0).rows(0)("AUTHCODE") & "' FROM V_BALANCE_INVOICE_TO_RECEIVE WHERE NVL(balance_amount,0)>0 AND INVOICE_NO='" & Invoice_no & "'")


                    End If

                    Dim ds1 As New Data.DataSet
                    ds1 = Return_record_set("SELECT * from SD_INVOICE_CC_PAYMENT where INVOICE_NO='" & Invoice_no & "'")


                    Dim str1 As String = SOAPManual(ds1.Tables(0), Session("USER_ID"), TLurl, TLactionURL, SECURITY_ID)
                    'ended on 25 AUGUST 2020
                    Dim reader As System.Xml.XmlTextReader = New System.Xml.XmlTextReader(New System.IO.StringReader(str1))
                    reader.Read()
                    Dim ds3 As New Data.DataSet
                    ds3.Clear()
                    ds3.ReadXml(reader)
					
					'Response.Write(ds3.Tables("runTransactionResult").Rows(0))
					'Exit Sub
                    If (ds3.Tables("runTransactionResult").Rows.Count > 0) Then
					
                        If (ds3.Tables("runTransactionResult").Rows(0)("Result") = "Approved") Then

                            'Response.Write(ds3.Tables("TranResult").Rows(0)("Result"))
                            'Exit Sub
                            'Update_query("Update SD_ORDER_CC_PAYMENT Set AUTHCODE='" & ds.Tables(0).Rows(0)("AUTHCODE") & "', PayByType='" & ds.Tables(0).Rows(0)("PAYBYTYPE") & "',CCType='" & ds.Tables(0).Rows(0)("CCTYPE") & "', MaskedCC='" & ds.Tables(0).Rows(0)("MASKEDCC") & "',TranRefNum='" & ds.Tables(0).Rows(0)("TRANREFNUM") & "',PmToken='" & ds.Tables(0).Rows(0)("PMTOKEN") & "',CustToken='" & ds.Tables(0).Rows(0)("CUSTTOKEN") & "',TranResultCode='" & ds.Tables(0).Rows(0)("TRANRESULTCODE") & "',PAYMENT_RESPONSE='" & ds.Tables(0).Rows(0)("PAYMENT_RESPONSE") & "',INVOICE_NO='" & ds.Tables(0).Rows(0)("INVOICE_NO") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where ORDER_NO='" & Request.QueryString.Get("ORDER_NO") & "'")

                            Update_query("Update SD_INVOICE_CC_PAYMENT Set STATUS='" & ds3.Tables("runTransactionResult").Rows(0)("Result") & "', AUTHCODE='" & ds.Tables(0).Rows(0)("AUTHCODE") & "', PayByType='" & ds.Tables(0).Rows(0)("PAYBYTYPE") & "',CCType='" & ds.Tables(0).Rows(0)("CCTYPE") & "', MaskedCC='" & ds.Tables(0).Rows(0)("MASKEDCC") & "',TranRefNum='" & ds.Tables(0).Rows(0)("TRANREFNUM") & "',PmToken='" & ds.Tables(0).Rows(0)("PMTOKEN") & "',CustToken='" & ds.Tables(0).Rows(0)("CUSTTOKEN") & "',TranResultCode='" & ds.Tables(0).Rows(0)("TRANRESULTCODE") & "',PAYMENT_RESPONSE='" & ds.Tables(0).Rows(0)("PAYMENT_RESPONSE") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where INVOICE_NO='" & Invoice_no & "'")
                            ' Update_query("UPDATE SD_INVOICE_CC_PAYMENT SET API_TOKEN='" & replace_c(ds3.Tables("GetEbizWebFormURLResponse").Rows(0)("GetEbizWebFormURLResult")) & "' WHERE INVOICE_NO='" & Invoice_no & "'")
                            Call execute_storeProcedure("SP_CREATE_CR_FROM_SD_INV", Invoice_no & "#" & ds1.Tables(0).Rows(0)("AMOUNT"), "P_INVOICE_NO#P_BAL_AMT", "C#N")
                            Dim strFile1 As String = "TEST"
                            Dim strCmd1 As String
                            strCmd1 = String.Format("alert('Processed Sucessfully.');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                        Else
                            Update_query("Update SD_INVOICE_CC_PAYMENT Set PAYMENT_RESPONSE='" & ds.Tables(0).Rows(0)("PAYMENT_RESPONSE") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE,status='" & replace_c(ds3.Tables("runTransactionResult").Rows(0)("Error")) & "' Where INVOICE_NO='" & Invoice_no & "'")
                            'Update_query("Update SD_INVOICE_CC_PAYMENT Set PAYMENT_RESPONSE='" & ds.Tables(0).Rows(0)("PAYMENT_RESPONSE") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where INVOICE_NO='" & ds.Tables(0).Rows(0)("INVOICE_NO") & "'")
                            Dim strFile1 As String = "TEST"
                            Dim strCmd1 As String
                            strCmd1 = String.Format("alert('" & replace_c(ds3.Tables("runTransactionResult").Rows(0)("Error")) & "');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                        End If
                       
                        Exit Sub
                   
                    End If


                Else
                    Dim strFile1 As String = "TEST"
                    Dim strCmd1 As String
                    strCmd1 = String.Format("alert('Please select Paid Transaction for Return.');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                    Exit Sub
                End If
            Else
                Dim strFile1 As String = "TEST"
                Dim strCmd1 As String
                strCmd1 = String.Format("alert('Please select Paid Transaction for Return.');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                Exit Sub

            End If
        End If


    End Sub
    Public Function SOAPManual(ByVal ds As Data.DataTable, ByVal USER_ID As String, ByVal url As String, ByVal action As String, ByVal TLSecurityID As String) As String

        'Const url As String = "https://soap.ebizcharge.net/eBizService.svc"
        'Const action As String = "http://eBizCharge.ServiceModel.SOAP/IeBizService/runTransaction"
        'Dim soapEnvelopeXml As XmlDocument = CreateSoapEnvelope(ds1, USER_ID)
        Dim soapEnvelopeXml As XmlDocument = CreateSoapEnvelope(ds, USER_ID, TLUserId, TLPassword, TLSecurityID)
        Dim webRequest As HttpWebRequest = CreateWebRequest(url, action)
        eBiz_Credit_Auth_Returns.InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest)
        Dim result As String

        Using response As WebResponse = webRequest.GetResponse()

            Using rd As StreamReader = New StreamReader(response.GetResponseStream())
                result = rd.ReadToEnd()
            End Using
        End Using

        Return result


    End Function

    Private Shared Function CreateWebRequest(ByVal url As String, ByVal action As String) As HttpWebRequest
        Dim webRequest As HttpWebRequest = CType(webRequest.Create(url), HttpWebRequest)
        webRequest.Headers.Add("SOAPAction", action)
        webRequest.ContentType = "text/xml;charset=""utf-8"""
        webRequest.Accept = "text/xml"
        webRequest.Method = "POST"
        Return webRequest
    End Function

    Private Shared Function CreateSoapEnvelope(ByRef ds As Data.DataTable, ByVal USER_ID As String, ByVal TL_USER_ID As String, ByVal TL_PASSWORD As String, ByVal TL_SECURITY As String) As XmlDocument

        Dim soapEnvelopeXml As XmlDocument = New XmlDocument()
        soapEnvelopeXml.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" &
        "<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ebiz=""http://eBizCharge.ServiceModel.SOAP"">" &
        "   <soapenv:Header/>" &
        "       <soapenv:Body>" &
        "           <ebiz:runTransaction>" &
        "               <ebiz:securityToken>" &
        "                   <ebiz:SecurityId>" & TL_SECURITY & "</ebiz:SecurityId>" &
        "                   <ebiz:UserId>" & TL_USER_ID & "</ebiz:UserId>" &
        "                   <ebiz:Password>" & TL_PASSWORD & "</ebiz:Password>" &
        "               </ebiz:securityToken>" &
        "               <ebiz:tran>" &
         "<ebiz:Command>capture</ebiz:Command>" &
         "                   <ebiz:RefNum>" & ds.ROWS(0)("TRANREFNUM").tostring() & "</ebiz:RefNum>" & _
        "                   <ebiz:Details>" &
        "	                    <ebiz:Amount>" & ds.Rows(0)("AMOUNT") & "</ebiz:Amount>" &
        "                   </ebiz:Details>" &
        "               </ebiz:tran>" &
        "          </ebiz:runTransaction>" &
        "       </soapenv:Body>" &
        "</soapenv:Envelope>")



        Return soapEnvelopeXml
    End Function

    Private Shared Sub InsertSoapEnvelopeIntoWebRequest(ByVal soapEnvelopeXml As XmlDocument, ByVal webRequest As HttpWebRequest)
        Using stream As Stream = webRequest.GetRequestStream()
            soapEnvelopeXml.Save(stream)
        End Using
    End Sub
    Public Function replace_c(ByVal p As String) As String
        p = Replace(p, "'", "''")
        Return p
    End Function
    Public Function replace_c4(ByVal p As String) As String
        If (Len(p) > 3997) Then
            p = Mid(p, 1, 3997)
        End If
        p = Replace(p, "'", "''")
        Return p
    End Function
    Public Function replace_c1(ByVal p As String) As String
        If (p <> "") Then
            If (IsNumeric(p)) Then
                Return p
            Else
                p = "null"
            End If
        Else
            p = "null"
        End If
        Return p
    End Function
    Public Sub execute_storeProcedure(ByVal fname As String, ByVal plist As String, ByVal plist1 As String, ByVal plist2 As String)
        Dim s As String
        s = ""
        Dim pu1, pu2, pu3
        Dim rct, pp As Integer
        If (plist <> "") Then
            pu1 = Split(plist, "#")
            pu2 = Split(plist1, "#")
            pu3 = Split(plist2, "#")
            If (UBound(pu1) > 0) Then
                rct = UBound(pu1)
            Else
                rct = 0
            End If
        Else
            rct = -1
        End If
        Dim rvalue As String
        dbad.SelectCommand.Connection = conn.getconnection()
        dbad.SelectCommand.Parameters.Clear()
        dbad.SelectCommand.CommandType = System.Data.CommandType.Text
        If (rct = -1) Then
            dbad.SelectCommand.CommandText = ("{call " & fname & "()}")
        Else
            If (rct = 0) Then
                dbad.SelectCommand.CommandText = ("{call " & fname & "(?)}")
            Else
                Dim rrr As String
                rrr = ""
                For pp = 0 To rct
                    rrr = rrr & "?" & ","
                Next
                rrr = Mid(rrr, 1, Len(rrr) - 1)
                dbad.SelectCommand.CommandText = ("{call " & fname & "(" & rrr & ")}")
            End If
        End If
        If (rct >= 0) Then
            If (rct = 0) Then
                If (UCase(plist2) = "N") Then
                    dbad.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter(plist1, System.Data.OleDb.OleDbType.Double, 20, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, plist))
                Else
                    dbad.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter(plist1, System.Data.OleDb.OleDbType.VarChar, 2000, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, plist))
                End If
            Else
                For pp = 0 To rct
                    If (UCase(pu3(pp)) = "N") Then
                        dbad.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter(pu2(pp), System.Data.OleDb.OleDbType.Double, 20, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, pu1(pp)))
                    Else
                        dbad.SelectCommand.Parameters.Add(New System.Data.OleDb.OleDbParameter(pu2(pp), System.Data.OleDb.OleDbType.VarChar, 2000, System.Data.ParameterDirection.Input, False, CType(0, Byte), CType(0, Byte), "", System.Data.DataRowVersion.Current, pu1(pp)))
                    End If
                Next
            End If
        End If
        If (dbad.SelectCommand.Connection.State = System.Data.ConnectionState.Closed) Then
            dbad.SelectCommand.Connection.Open()
        End If
        Try
            dbad.SelectCommand.ExecuteNonQuery()
        Catch exException As Exception
            dbad.SelectCommand.Connection.Close()
        End Try
        dbad.SelectCommand.Connection.Close()
    End Sub

    Public Sub Update_query(ByVal str_update As String)
        Try
            dbad.UpdateCommand = New OleDbCommand
            dbad.UpdateCommand.Connection = conn.getconnection()
            dbad.UpdateCommand.CommandText = str_update
            dbad.UpdateCommand.ExecuteNonQuery()
            dbad.UpdateCommand.Connection.Close()
        Catch ex As Exception
            Call insert_sys_log("Update_query", ex.Message)
        End Try
    End Sub
    Public Sub Insert_query(ByVal str_insert As String)
        Try
            dbad.InsertCommand = New OleDbCommand
            dbad.InsertCommand.Connection = conn.getconnection()
            dbad.InsertCommand.CommandText = str_insert
            dbad.InsertCommand.ExecuteNonQuery()
            dbad.InsertCommand.Connection.Close()
        Catch ex As Exception
            Call insert_sys_log("Insert_query", ex.Message)
        End Try
    End Sub
    Public Sub Delete_query(ByVal str_delete As String)
        Try
            dbad.DeleteCommand = New OleDbCommand
            dbad.DeleteCommand.Connection = conn.getconnection()
            dbad.DeleteCommand.CommandText = str_delete
            dbad.DeleteCommand.ExecuteNonQuery()
            dbad.DeleteCommand.Connection.Close()
        Catch ex As Exception
            Call insert_sys_log("Delete_query", ex.Message)
        End Try
    End Sub
    Public Function Return_record_count(ByVal str_select As String) As Integer
        Try
            Dim ds_new As New System.Data.DataSet
            dbad.SelectCommand = New OleDbCommand
            dbad.SelectCommand.Connection = conn.getconnection()
            dbad.SelectCommand.CommandText = str_select
            dbad.Fill(ds_new)
            dbad.SelectCommand.Connection.Close()
            If (ds_new.Tables(0).Rows.Count > 0) Then
                Return 1
            Else
                Return 0
            End If
            Return 0
        Catch ex As Exception
            Call insert_sys_log("Return_record_count", ex.Message)
        End Try
    End Function
    Public Function Return_record_set(ByVal str_select As String) As System.Data.DataSet
        Try
            Dim ds_new As New System.Data.DataSet
            dbad.SelectCommand = New OleDbCommand
            dbad.SelectCommand.Connection = conn.getconnection()
            dbad.SelectCommand.CommandText = str_select
            dbad.Fill(ds_new)
            dbad.SelectCommand.Connection.Close()
            Return ds_new
        Catch ex As Exception
            Call insert_sys_log("Return_record_set", ex.Message)
        End Try
    End Function
    Public Sub insert_sys_log(ByVal str1 As String, ByVal message As String)
        Dim sterr1, sterr2, sterr3, sterr4, sterr As String
        sterr = Replace(message, "'", "''")
        If (Len(sterr) > 4000) Then
            sterr1 = Mid(sterr, 1, 4000)
            If (Len(sterr) > 8000) Then
                sterr2 = Mid(sterr, 4000, 8000)
                If (Len(sterr) > 12000) Then
                    sterr3 = Mid(sterr, 8000, 12000)
                    If (Len(sterr) > 16000) Then
                        sterr4 = Mid(sterr, 12000, 16000)
                    Else
                        sterr4 = Mid(sterr, 12000, Len(sterr))
                    End If
                Else
                    sterr3 = Mid(sterr, 8000, Len(sterr))
                    sterr4 = ""
                End If
            Else
                sterr2 = Mid(sterr, 4000, Len(sterr))
                sterr3 = ""
                sterr3 = ""
                sterr4 = ""
            End If
        Else
            sterr1 = sterr
            sterr2 = ""
            sterr3 = ""
            sterr4 = ""
        End If
        Try
            dbad.InsertCommand.Connection = conn.getconnection()
            If (dbad.InsertCommand.Connection.State = System.Data.ConnectionState.Closed) Then
                dbad.InsertCommand.Connection.Open()
            End If
            dbad.InsertCommand.CommandText = "Insert into SYS_ACTIVATE_STATUS_LOG (LINE_NO, CHANGE_REQUEST_NO,  OBJECT_TYPE, OBJECT_NAME, ERROR_TEXT, STATUS,LOG_DATE,ERROR_TEXT1, ERROR_TEXT2, ERROR_TEXT3) values ((select nvl(max(to_number(line_no)),0)+1 from SYS_ACTIVATE_STATUS_LOG),'','EBIZ_PAYMENT','" & str1 & "','" & sterr1 & "','N',sysdate,'" & sterr2 & "','" & sterr3 & "','" & sterr4 & "')"
            dbad.InsertCommand.ExecuteNonQuery()
            dbad.InsertCommand.Connection.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
