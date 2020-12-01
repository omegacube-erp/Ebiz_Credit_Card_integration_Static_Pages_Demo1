
Partial Class eBiz_cc_sales_responce
    Inherits System.Web.UI.Page
    Protected dbad As New OleDbDataAdapter
    Public conn As New Dbconn
	Public EXTRA_CHARGE,DEFAULT_SHIPPING_CHARGE,ORDER_NO,line_no As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dbad.SelectCommand = New OleDbCommand
        dbad.InsertCommand = New OleDbCommand
        dbad.UpdateCommand = New OleDbCommand
        Try

 If Not Request.QueryString.Get("TransactionLookupKey") Is Nothing Then
                'SESSION("DOCID")=Request.QueryString("ORDER_NO")
                'SESSION("LINENO")=1
                ORDER_NO = Request.QueryString.Get("TransactionLookupKey")
                line_no = "1"
            End If

            'If (Request.QueryString.Get("TransactionLookupKey") <> "") Then
            '    Update_query("Update SD_INVOICE_CC_PAYMENT Set PAYMENT_RESPONSE='" & Request.QueryString.Get("RESPONSE_TYPE") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where INVOICE_NO='" & ds1.Tables(0).Rows(0)("INVOICE_NO").ToString & "'")
            'End If

            If (Session("user_id") Is Nothing) Then
                Session("user_id") = "OCT-EMAIL"
            End If
			'Added Code for Calculating the Amount like Xtra Charges and Default Shipping Address on 7 OCT 2020 by deepu
				 Try
                    Dim dsxtracharge As New Data.DataSet
                    dsxtracharge = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='PERCENT_OF_EXTRA_CHARGES'")
                    dsxtracharge.Clear()

                    dbad.Fill(dsxtracharge)

                    If (dsxtracharge.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsxtracharge.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            EXTRA_CHARGE = dsxtracharge.Tables(0).Rows(0)(0)
                        Else
                            EXTRA_CHARGE = ""
                        End If
                    End If
                Catch ex As Exception
                    EXTRA_CHARGE = "0"
                End Try
				
				
				
				 Try
                    Dim dsdefshicharge As New Data.DataSet
                    dsdefshicharge = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='DEFAULT_SHIPPING_CHARGE'")
                    dsdefshicharge.Clear()

                    dbad.Fill(dsdefshicharge)

                    If (dsdefshicharge.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsdefshicharge.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            DEFAULT_SHIPPING_CHARGE = dsdefshicharge.Tables(0).Rows(0)(0)
                        Else
                            DEFAULT_SHIPPING_CHARGE = ""
                        End If
                    End If
                Catch ex As Exception
                    DEFAULT_SHIPPING_CHARGE = "50"
                End Try
				
				
				
				
            Dim ds12 As New Data.DataSet
            If ORDER_NO = "" Then
                ds12 = Return_record_set("SELECT ORDER_NO,LINE_NO,round(AMOUNT,2) AMOUNT,PAYMENT_RESPONSE,AUTHCODE,AUTH_TRANS_USER,AUTH_TRANS_ID,AUTH_TRANS_DATE,API_TOKEN,API_TOKEN_SESSION,CHANGED_BY,CHANGED_DATE,CREATED_BY,CREATED_DATE FROM SD_ORDER_CC_PAYMENT Where AUTH_TRANS_DATE > sysdate-1/10 AND PAYMENT_RESPONSE='IN_PROCESS' AND UPPER(AUTH_TRANS_USER)=UPPER('" & Session("user_id").ToString & "')")
            Else
                ds12 = Return_record_set("SELECT ORDER_NO,LINE_NO,round(AMOUNT,2) ,AMOUNT,PAYMENT_RESPONSE,AUTHCODE,AUTH_TRANS_USER,AUTH_TRANS_ID,AUTH_TRANS_DATE,API_TOKEN,API_TOKEN_SESSION,CHANGED_BY,CHANGED_DATE,CREATED_BY,CREATED_DATE FROM SD_ORDER_CC_PAYMENT Where ORDER_NO='" & ORDER_NO & "' AND LINE_NO=" & line_no)


            End If
            ds12.Clear()
            dbad.Fill(ds12)
				
				
				
				
				'Ended Code for Calculating the Amount like Xtra Charges and Default Shipping Address on 7 OCT 2020 by deepu

            If (Request.QueryString.Get("TranResult") = "Approved") Then
			 
                If (ds12.Tables(0).Rows.Count > 0) Then
                   'added by deepu on 7 OCT 2020
                    Dim calamount As String
                    If (ds12.Tables(0).Rows(0)("AMOUNT") > 0) Then

                        calamount = Trim(((ds12.Tables(0).Rows(0)("AMOUNT")) * EXTRA_CHARGE / 100) + DEFAULT_SHIPPING_CHARGE)

                    Else
                        calamount = ds12.Tables(0).Rows(0)("AMOUNT")
                    End If
                    'ended by deepu on 7 OCT 2020

                    'ended by deepu on 7 OCT 2020


                    Update_query("Update SD_ORDER_CC_PAYMENT Set AUTHCODE='" & Request.QueryString.Get("AuthCode") & "', PayByType='" & Request.QueryString.Get("PayByType") & "',CCType='" & Request.QueryString.Get("CCType") & "', MaskedCC='" & Request.QueryString.Get("MaskedCC") & "',TranRefNum='" & Request.QueryString.Get("TranRefNum") & "',PmToken='" & Request.QueryString.Get("PmToken") & "',CustToken='" & Request.QueryString.Get("CustToken") & "',TranResultCode='" & Request.QueryString.Get("TranResultCode") & "',PAYMENT_RESPONSE='" & Request.QueryString.Get("TranResult") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where ORDER_NO='" & Request.QueryString.Get("TransactionLookupKey") & "'")
                    'Call execute_storeProcedure("SP_CREATE_CR_FROM_SD_ORDER", ds12.Tables(0).Rows(0)("ORDER_NO").ToString & "#" & calamount, "P_ORDER_NO#P_BAL_AMT", "C#N")
                Else
				     Update_query("Update SD_ORDER_CC_PAYMENT Set PAYMENT_RESPONSE='" & Request.QueryString.Get("TranResult") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where ORDER_NO='" & Request.QueryString.Get("TransactionLookupKey") & "'")
                End If
                

                'Dim ds1 As New Data.DataSet
                'ds1.Clear()
               ' ds1 = Return_record_set("SELECT ORDER_NO,LINE_NO,round(AMOUNT,2) AMOUNT,PAYMENT_RESPONSE,AUTHCODE,AUTH_TRANS_USER,AUTH_TRANS_ID,AUTH_TRANS_DATE,API_TOKEN,API_TOKEN_SESSION,CHANGED_BY,CHANGED_DATE,CREATED_BY,CREATED_DATE FROM SD_ORDER_CC_PAYMENT Where ORDER_NO='" & Request.QueryString.Get("TransactionLookupKey") & "'")
               ' If (ds1.Tables(0).Rows.Count > 0) Then
                  '  Call execute_storeProcedure("SP_CREATE_CR_FROM_SD_INV", ds1.Tables(0).Rows(0)("ORDER_NO").ToString & "#" & ds1.Tables(0).Rows(0)("AMOUNT"), "P_INVOICE_NO#P_BAL_AMT", "C#N")
                'End If
            Else

                Update_query("Update SD_ORDER_CC_PAYMENT Set PAYMENT_RESPONSE='" & Request.QueryString.Get("TranResult") & "',CHANGED_DATE=SYSDATE,AUTH_TRANS_USER= UPPER('" & Session("user_id").ToString & "'),AUTH_TRANS_DATE=SYSDATE Where ORDER_NO='" & Request.QueryString.Get("TransactionLookupKey") & "'")

            End If
            If (Request.QueryString.Get("PTYPE") = "EMAIL") Then
                Response.Redirect("eBIZ_message.aspx")
            Else
                Dim strFile1 As String = "TEST"
                Dim strCmd1 As String
                'strCmd1 = String.Format("window.opener.document.getElementById('Retrive').click();window.close();", strFile1)
                strCmd1 = String.Format("window.close();", strFile1)
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
            End If

        Catch ex As Exception
            Call insert_sys_log("Close Button", ex.Message)
        End Try
    End Sub
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
            dbad.InsertCommand.CommandText = "Insert into SYS_ACTIVATE_STATUS_LOG (LINE_NO, CHANGE_REQUEST_NO,  OBJECT_TYPE, OBJECT_NAME, ERROR_TEXT, STATUS,LOG_DATE,ERROR_TEXT1, ERROR_TEXT2, ERROR_TEXT3) values ((select nvl(max(to_number(line_no)),0)+1 from SYS_ACTIVATE_STATUS_LOG),'','AUTHORIZE_PAYMENT','" & str1 & "','" & sterr1 & "','N',sysdate,'" & sterr2 & "','" & sterr3 & "','" & sterr4 & "')"
            dbad.InsertCommand.ExecuteNonQuery()
            dbad.InsertCommand.Connection.Close()
        Catch ex As Exception
        End Try
    End Sub
End Class
