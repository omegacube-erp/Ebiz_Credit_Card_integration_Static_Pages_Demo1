Imports System.IO
Imports System.Net
Imports System.Xml
Partial Class Ebiz_integration
    Inherits System.Web.UI.Page
    Protected dbad As New OleDbDataAdapter
    Public conn As New Dbconn
    Public Invoice_no, line_no, resp, ptype As String
    Public TLurl, TLactionURL, TLPassword, TLUserId, SECURITY_ID, CLIENT_URL, FROM_NAME, FROM_EMAIL As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        dbad.SelectCommand = New OleDbCommand
        dbad.InsertCommand = New OleDbCommand
        dbad.UpdateCommand = New OleDbCommand
        dbad.DeleteCommand = New OleDbCommand
        If (Session("user_id") = "") Then
            Response.Redirect("LOGIN.ASPX")
        End If
        'Data.datatable

        If Not IsPostBack Then


            If Not Request.QueryString("INVOICE_NO") Is Nothing Then
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
                    dsactionurl = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_ACTION_URL_INTEGRATION'")
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
                    TLactionURL = "http://eBizCharge.ServiceModel.SOAP/IeBizService/GetEbizWebFormURL"
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
				dssecurity=Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_SECURITY_ID'")
				dssecurity.Clear()
				dbad.Fill(dssecurity)
				If(dssecurity.Tables(0).Rows.Count>0)Then
				If Not (Equals(dssecurity.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
				SECURITY_ID=dssecurity.Tables(0).Rows(0)(0)
				Else
				SECURITY_ID=""
				End If
				End If
				Catch ex As Exception
                                                                        SECURITY_ID = "8303a4ef-025c-439a-97f7-6b79bc2aff16"
                                                                    End Try


                'getting Client URL from Tool Settings
                Try
                    Dim dsclienturl As New Data.DataSet
                    dsclienturl = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_CLIENT_URL'")
                    dsclienturl.Clear()
                    dbad.Fill(dsclienturl)
                    If (dsclienturl.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsclienturl.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            CLIENT_URL = dsclienturl.Tables(0).Rows(0)(0)
                        Else
                            CLIENT_URL = ""
                        End If
                    End If
                Catch ex As Exception
                    CLIENT_URL = "https://erpdoublestarusa.com:8443/"
                End Try

                'getting FromEmail from Tool Settings added by deepu on 25 SEP 2020
                Try
                    Dim dsfromemail As New Data.DataSet
                    dsfromemail = Return_record_set("select VALUE from SYS_TOOL_SETTINGS where KEY ='EBIZ_FROM_EMAIL'")
                    dsfromemail.Clear()
                    dbad.Fill(dsfromemail)
                    If (dsfromemail.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsfromemail.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            FROM_EMAIL = dsfromemail.Tables(0).Rows(0)(0)
                        Else
                            FROM_EMAIL = ""
                        End If
                    End If
                Catch ex As Exception
                    FROM_EMAIL = "rreddy@omegacube.com"
                End Try


                Try
                    Dim dsfromname As New Data.DataSet
                    dsfromname = Return_record_set("SELECT NAME  FROM GL_BUSINESS_UNITS WHERE BUSINESS_UNIT_CODE=SF_GET_DEFAULT_BUC")
                    dsfromname.Clear()

                    dbad.Fill(dsfromname)

                    If (dsfromname.Tables(0).Rows.Count > 0) Then
                        If Not (Equals(dsfromname.Tables(0).Rows(0)(0), System.DBNull.Value)) Then
                            'FROM_NAME = dsfromname.Tables(0).Rows(0)(0)
							FROM_NAME = Replace(dsfromname.Tables(0).Rows(0)(0),"&","&amp;")
                        Else
                            FROM_NAME = ""
                        End If
                    End If
                Catch ex As Exception
                    FROM_NAME = "ABC Company"
                End Try

                'getting FromEmail from Tool Settings added by deepu on 25 SEP 2020


                'ended Code for getting URL and Action Id from TOOL Settings on 27 AUGUST 2020 by deepu
                Invoice_no = Request.QueryString.Get("INVOICE_NO")
                ptype = Request.QueryString.Get("PAYMENT_TYPE")
                Dim dsTransacId As New Data.DataSet
                dsTransacId = Return_record_set("select * from SD_INVOICE_CC_PAYMENT where INVOICE_NO='" & Invoice_no & "' AND AUTHCODE IS NOT NULL")
                dsTransacId.Clear()
                dbad.Fill(dsTransacId)
                If (dsTransacId.Tables(0).Rows.Count > 0) Then
                    ' Insert_query("INSERT INTO SD_INVOICE_CC_PAYMENT (INVOICE_NO,LINE_NO,AMOUNT,PAYMENT_TYPE,API_TOKEN,CREATED_BY,CREATED_DATE) select INVOICE_NO,1,balance_amount,'" & ptype & "','','" & Session("USER_ID") & "',SYSDATE FROM V_BALANCE_INVOICE_TO_RECEIVE WHERE NVL(balance_amount,0)>0 AND INVOICE_NO='" & Invoice_no & "'")
                    Dim strFile1 As String = "TEST"
                    Dim strCmd1 As String
                    strCmd1 = String.Format("alert('This invoice already Processed');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                    ' strCmd1 = String.Format("window.close();", strFile1)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                    Exit Sub
                Else
                    Delete_query("DELETE FROM SD_INVOICE_CC_PAYMENT where INVOICE_NO='" & Invoice_no & "'")
                    Insert_query("INSERT INTO SD_INVOICE_CC_PAYMENT (INVOICE_NO,LINE_NO,AMOUNT,PAYMENT_TYPE,API_TOKEN,CREATED_BY,CREATED_DATE) select INVOICE_NO,1,balance_amount,'" & ptype & "','','" & Session("USER_ID") & "',SYSDATE FROM V_BALANCE_INVOICE_TO_RECEIVE WHERE NVL(balance_amount,0)>0 AND INVOICE_NO='" & Invoice_no & "'")


                End If
                Dim ds As New Data.DataSet
				'added by deepu on 28 OCt 2020
                'ds = Return_record_set("SELECT A.INVOICE_NO,A.LINE_NO,round(A.AMOUNT,2) AMOUNT,b.Customer_no, b.EMAIL,b.NAME,NVL(b.BILLING_NAME,B.NAME) BILLING_NAME, NVL(b.BILLING_STREET,b.STREET) BILLING_STREET,b.STREET, NVL(b.BILLING_CITY,b.CITY) BILLING_CITY,b.CITY,NVL(b.BILLING_STATE_CODE,b.STATE_CODE) BILLING_STATE_CODE,b.STATE_CODE,NVL(b.BILLING_POSTAL_CODE,b.POSTAL_CODE) BILLING_POSTAL_CODE,b.POSTAL_CODE, NVL(b.BILLING_COUNTRY,b.COUNTRY) BILLING_COUNTRY ,b.COUNTRY,b.order_no,NVL(B.DIFFERENT_BILLING_ADDRESS,'N') DIFFERENT_BILLING_ADDRESS,B.CUSTOMER_PO_NO,to_char(B.INVOICE_DATE,'YYYY-MM-DD')||'T00:00:00-00:00' INVOICE_DATE,to_char(B.INVOICE_DUE_DATE,'YYYY-MM-DD')||'T00:00:00-00:00' INVOICE_DUE_DATE,b.CONTACT_NAME,D.INVOICE_NO || '-' ||D.LINE_NO AS LINE_NO,D.ITEM_NO,D.DESCRIPTION,D.QTY,D.SALES_PRICE,D.PRICING_UOM,case when nvl(D.TAX_AMOUNT,0)>0 then 'Y' ELSE 'N' END AS TAXABLE,nvl(D.TAX_AMOUNT,0) tax_amount,D.LINE_TOTAL_AMOUNT,case when nvl(D.TAX_AMOUNT,0)>0 then NVL((SELECT X.TAX_PERCENTAGE  FROM GE_STATE_CITY_TAX_PERCENTAGE X WHERE X.TAX_CODE=D.TAX_CODE),0) else 0 end as TAX_PER  FROM SD_INVOICE_CC_PAYMENT A, SD_INVOICE_HEADER b,sd_customers C,SD_INVOICE_DETAIL d WHERE A.INVOICE_NO=B.INVOICE_NO and b.invoice_no=d.invoice_no  and b.CUSTOMER_NO=C.account_no and  A.INVOICE_NO='" & Invoice_no & "' ")
				
				ds = Return_record_set("SELECT A.INVOICE_NO,A.LINE_NO,round(A.AMOUNT,2) AMOUNT,sf_get_cc_customer_no(b.Customer_no) AS Customer_no, b.EMAIL,b.NAME,NVL(b.BILLING_NAME,B.NAME) BILLING_NAME, NVL(b.BILLING_STREET,b.STREET) BILLING_STREET,b.STREET, NVL(b.BILLING_CITY,b.CITY) BILLING_CITY,b.CITY,NVL(b.BILLING_STATE_CODE,b.STATE_CODE) BILLING_STATE_CODE,b.STATE_CODE,NVL(b.BILLING_POSTAL_CODE,b.POSTAL_CODE) BILLING_POSTAL_CODE,b.POSTAL_CODE, NVL(b.BILLING_COUNTRY,b.COUNTRY) BILLING_COUNTRY ,b.COUNTRY,b.order_no,NVL(B.DIFFERENT_BILLING_ADDRESS,'N') DIFFERENT_BILLING_ADDRESS,B.CUSTOMER_PO_NO,to_char(B.INVOICE_DATE,'YYYY-MM-DD')||'T00:00:00-00:00' INVOICE_DATE,to_char(B.INVOICE_DUE_DATE,'YYYY-MM-DD')||'T00:00:00-00:00' INVOICE_DUE_DATE,b.CONTACT_NAME,D.INVOICE_NO || '-' ||D.LINE_NO AS LINE_NO,D.ITEM_NO,D.DESCRIPTION,D.QTY,D.SALES_PRICE,D.PRICING_UOM,case when nvl(D.TAX_AMOUNT,0)>0 then 'Y' ELSE 'N' END AS TAXABLE,nvl(D.TAX_AMOUNT,0) tax_amount,D.LINE_TOTAL_AMOUNT,case when nvl(D.TAX_AMOUNT,0)>0 then NVL((SELECT X.TAX_PERCENTAGE  FROM GE_STATE_CITY_TAX_PERCENTAGE X WHERE X.TAX_CODE=D.TAX_CODE),0) else 0 end as TAX_PER  FROM SD_INVOICE_CC_PAYMENT A, SD_INVOICE_HEADER b,sd_customers C,SD_INVOICE_DETAIL d WHERE A.INVOICE_NO=B.INVOICE_NO and b.invoice_no=d.invoice_no  and b.CUSTOMER_NO=C.account_no and  A.INVOICE_NO='" & Invoice_no & "' ")

                If ds.Tables(0).Rows.Count > 0 Then

                    'Dim str1 As String = SOAPManual(ds.Tables(0), Session("USER_ID"), ptype)
                    'added on 25 AUGUST 2020
                    Dim str1 As String = SOAPManual(ds.Tables(0), Session("USER_ID"), ptype, TLurl, TLactionURL, SECURITY_ID, CLIENT_URL, FROM_EMAIL, FROM_NAME)
                    'ended on 25 AUGUST 2020
                    Dim reader As System.Xml.XmlTextReader = New System.Xml.XmlTextReader(New System.IO.StringReader(str1))
                    reader.Read()


                    Dim ds3 As New Data.DataSet
                    ds3.Clear()
                    ds3.ReadXml(reader)
                    If (ds3.Tables("GetEbizWebFormURLResponse").Rows.Count > 0) Then
                        Update_query("UPDATE SD_INVOICE_CC_PAYMENT SET API_TOKEN='" & replace_c(ds3.Tables("GetEbizWebFormURLResponse").Rows(0)("GetEbizWebFormURLResult")) & "' WHERE INVOICE_NO='" & Invoice_no & "'")
                        If (ptype = "CC" OR ptype="ONLINE") Then
                            Response.Redirect(ds3.Tables("GetEbizWebFormURLResponse").Rows(0)("GetEbizWebFormURLResult"))
                        Else
                            Dim strFile1 As String = "TEST"
                            Dim strCmd1 As String
                            strCmd1 = String.Format("alert('Sucessfully Sent Email.');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                            ' strCmd1 = String.Format("window.close();", strFile1)
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                            Exit Sub
                        End If
                    End If


                Else
                    Dim strFile1 As String = "TEST"
                    Dim strCmd1 As String
                    strCmd1 = String.Format("alert('This invoice is fully paid.');window.opener.document.getElementById('" & Request.QueryString.Get("Retrive_id") & "').click();window.close();", strFile1)
                    ' strCmd1 = String.Format("window.close();", strFile1)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "GEN1", strCmd1, True)
                    Exit Sub
                End If


            End If
        End If


    End Sub
    Public Function SOAPManual(ByVal ds1 As Data.DataTable, ByVal USER_ID As String, ByVal ptype As String, ByVal url As String, ByVal action As String, ByVal TLSecurityID As String, ByVal TLClientURL As String, ByVal FromEmail As String, ByVal FromName As String) As String

        'Const url As String = "https://soap.ebizcharge.net/eBizService.svc"
        'Const action As String = "http://eBizCharge.ServiceModel.SOAP/IeBizService/GetEbizWebFormURL"
        'Dim soapEnvelopeXml As XmlDocument = CreateSoapEnvelope(ds1, USER_ID, ptype)
        Dim soapEnvelopeXml As XmlDocument = CreateSoapEnvelope(ds1, USER_ID, ptype, TLUserId, TLPassword, TLSecurityID, TLClientURL, FromEmail, FromName)
        Dim webRequest As HttpWebRequest = CreateWebRequest(url, action)
        Ebiz_integration.InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest)
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

    Private Shared Function CreateSoapEnvelope(ByRef ds1 As Data.DataTable, ByVal USER_ID As String, ByVal ptype As String, ByVal TL_USER_ID As String, ByVal TL_PASSWORD As String, ByVal TL_SECURITY As String, ByVal TL_CLIENT As String, ByVal TL_FROM_EMAIL As String, ByVal FromName As String) As XmlDocument
        Dim x1 As String
        If (ptype = "CC") Then
            x1 = "false"
        Else
            x1 = "true"
        End If
        Dim item_details As String
        item_details = "<ebiz:LineItems>"
        For i = 0 To ds1.Rows.Count - 1
            item_details = item_details & "<ebiz:TransactionLineItem>"
            item_details = item_details & "<ebiz:ProductRefNum>" & ds1.Rows(i)("ITEM_NO").ToString() & "</ebiz:ProductRefNum>"
            item_details = item_details & "<ebiz:SKU>" & ds1.Rows(i)("ITEM_NO").ToString() & "</ebiz:SKU>"
            item_details = item_details & "<ebiz:ProductName>" & Replace(ds1.Rows(i)("DESCRIPTION").ToString(),"&","&amp;") & "</ebiz:ProductName>"
            item_details = item_details & "<ebiz:Description>" & Replace(ds1.Rows(i)("DESCRIPTION").ToString(),"&","&amp;") & "</ebiz:Description>"
            item_details = item_details & "<ebiz:DiscountAmount>0.00</ebiz:DiscountAmount>"
            item_details = item_details & "<ebiz:DiscountRate>0.00</ebiz:DiscountRate>"
            item_details = item_details & "<ebiz:UnitOfMeasure>" & ds1.Rows(i)("PRICING_UOM").ToString() & "</ebiz:UnitOfMeasure>"
            item_details = item_details & "<ebiz:UnitPrice>" & ds1.Rows(i)("SALES_PRICE").ToString() & "</ebiz:UnitPrice>"
            item_details = item_details & "<ebiz:Qty>" & ds1.Rows(i)("QTY").ToString() & "</ebiz:Qty>"
            If (ds1.Rows(i)("TAXABLE").ToString() = "Y") Then
                item_details = item_details & "<ebiz:Taxable>true</ebiz:Taxable>"
            Else
                item_details = item_details & "<ebiz:Taxable>false</ebiz:Taxable>"
            End If

            item_details = item_details & "<ebiz:TaxAmount>" & ds1.Rows(i)("TAX_AMOUNT").ToString() & "</ebiz:TaxAmount>"
            item_details = item_details & "<ebiz:TaxRate>" & ds1.Rows(i)("TAX_PER").ToString() & "</ebiz:TaxRate>/ebiz:UnitPrice>"
            item_details = item_details & "</ebiz:TransactionLineItem>"
        Next
        item_details = item_details & "</ebiz:LineItems>"
        Dim soapEnvelopeXml As XmlDocument = New XmlDocument()
        soapEnvelopeXml.LoadXml("<?xml version=""1.0"" encoding=""utf-8""?>" &
        "<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:ebiz=""http://eBizCharge.ServiceModel.SOAP"">" &
        "<soapenv:Header/>" &
        "  <soapenv:Body>" &
        "      <ebiz:GetEbizWebFormURL>" &
        "        <ebiz:securityToken>" &
        "         <ebiz:SecurityId>" & TL_SECURITY & "</ebiz:SecurityId>" &
        "          <ebiz:UserId>" & TL_USER_ID & "</ebiz:UserId>" &
        "           <ebiz:Password>" & TL_PASSWORD & "</ebiz:Password>" &
        "         </ebiz:securityToken>" &
        "         <ebiz:ePaymentForm>" &
        "         <ebiz:FormType>WebForm</ebiz:FormType>" &
        "            <ebiz:FromEmail>" & TL_FROM_EMAIL & "</ebiz:FromEmail>" &
        "            <ebiz:FromName>" & FromName & " Payment Process</ebiz:FromName>" &
        "            <ebiz:EmailAddress>" & ds1.Rows(0)("EMAIL").ToString() & "</ebiz:EmailAddress>" &
        "            <ebiz:CcEmailAddress/>" &
        "            <ebiz:BccEmailAddress/>" &
        "            <ebiz:ReplyToEmailAddress>" & TL_FROM_EMAIL & "</ebiz:ReplyToEmailAddress>" &
        "            <ebiz:ReplyToDisplayName>Omegacube Support</ebiz:ReplyToDisplayName>" &
        "            <ebiz:EmailNotes>Invoice Payment</ebiz:EmailNotes>" &
        "            <ebiz:EmailSubject>Payment for Invoice No: " & ds1.Rows(0)("INVOICE_NO").ToString() & "</ebiz:EmailSubject>" &
        "            <ebiz:EmailTemplateID/>" &
        "            <ebiz:EmailTemplateName/>" &
        "            <ebiz:SendEmailToCustomer>" & x1 & "</ebiz:SendEmailToCustomer>" &
        "            <ebiz:CustomerId>" & Replace(ds1.Rows(0)("CUSTOMER_NO").ToString(),"&","&amp;") & "</ebiz:CustomerId>" &
        "            <ebiz:CustFullName>" & Replace(ds1.Rows(0)("NAME").ToString(),"&","&amp;") & "</ebiz:CustFullName>" &
        "            <ebiz:TransId>" & ds1.Rows(0)("INVOICE_NO").ToString() & "</ebiz:TransId>" &
        "            <ebiz:TransDetail>New Payment</ebiz:TransDetail>" &
        "            <ebiz:InvoiceNumber>" & ds1.Rows(0)("INVOICE_NO").ToString() & "</ebiz:InvoiceNumber>" &
        "            <ebiz:PoNum>" & ds1.Rows(0)("CUSTOMER_PO_NO").ToString() & "</ebiz:PoNum>" &
        "            <ebiz:SoNum>" & ds1.Rows(0)("ORDER_NO").ToString() & "</ebiz:SoNum>" &
        "            <ebiz:OrderId>" & ds1.Rows(0)("ORDER_NO").ToString() & "</ebiz:OrderId>" &
        "            <ebiz:Date>" & ds1.Rows(0)("INVOICE_DATE").ToString() & "</ebiz:Date>" &
        "            <ebiz:DueDate>" & ds1.Rows(0)("INVOICE_DUE_DATE").ToString() & "</ebiz:DueDate>" &
        "            <ebiz:TotalAmount>" & ds1.Rows(0)("AMOUNT").ToString() & "</ebiz:TotalAmount>" &
        "            <ebiz:AmountDue>" & ds1.Rows(0)("AMOUNT").ToString() & "</ebiz:AmountDue>" &
        "            <ebiz:TipAmount>0</ebiz:TipAmount>" &
        "            <ebiz:ShippingAmount>0</ebiz:ShippingAmount>" &
        "            <ebiz:DutyAmount>0</ebiz:DutyAmount>" &
        "            <ebiz:TaxAmount>0</ebiz:TaxAmount>" &
        "            <ebiz:Description>Payment</ebiz:Description>" &
        "            <ebiz:BillingAddress>" &
        "            <ebiz:FirstName>" & ds1.Rows(0)("CONTACT_NAME").ToString() & "</ebiz:FirstName>" &
        "            <ebiz:LastName>" & ds1.Rows(0)("CONTACT_NAME").ToString() & "</ebiz:LastName>" &
        "            <ebiz:CompanyName>" & Replace(ds1.Rows(0)("BILLING_NAME").ToString(),"&","&amp;") & "</ebiz:CompanyName>" &
        "            <ebiz:Address1>" & Replace(ds1.Rows(0)("BILLING_STREET").ToString(),"&","&amp;") & "</ebiz:Address1>" &
        "            <ebiz:Address2></ebiz:Address2>" &
        "            <ebiz:Address3/>" &
        "            <ebiz:City>" & ds1.Rows(0)("BILLING_CITY").ToString() & "</ebiz:City>" &
        "            <ebiz:State>" & ds1.Rows(0)("BILLING_STATE_CODE").ToString() & "</ebiz:State>" &
        "            <ebiz:ZipCode>" & ds1.Rows(0)("BILLING_POSTAL_CODE").ToString() & "</ebiz:ZipCode>" &
        "            <ebiz:Country>" & ds1.Rows(0)("BILLING_COUNTRY").ToString() & "</ebiz:Country>" &
        "            <ebiz:IsDefault>true</ebiz:IsDefault>" &
        "            <ebiz:AddressId>" & Replace(ds1.Rows(0)("CUSTOMER_NO").ToString(),"&","&amp;") & "_001</ebiz:AddressId>" &
        "            </ebiz:BillingAddress>" &
        "            <ebiz:ApprovedURL>" & TL_CLIENT & "eBiz_cc_responce.aspx?PTYPE=" & ptype & "</ebiz:ApprovedURL>" &
        "            <ebiz:DeclinedURL>" & TL_CLIENT & "eBiz_cc_responce.aspx?PTYPE=" & ptype & "</ebiz:DeclinedURL>" &
        "            <ebiz:ErrorURL>" & TL_CLIENT & "eBiz_cc_responce.aspx?PTYPE=" & ptype & "</ebiz:ErrorURL>" &
        "            <ebiz:DisplayDefaultResultPage>0</ebiz:DisplayDefaultResultPage>" &
        "            <ebiz:PayByType>CC</ebiz:PayByType>" &
        "            <ebiz:AllowedPaymentMethods/>" &
        "            <ebiz:SavePaymentMethod>true</ebiz:SavePaymentMethod>" &
        "            <ebiz:ShowSavedPaymentMethods>true</ebiz:ShowSavedPaymentMethods>" &
        "            <ebiz:CountryCode>USA</ebiz:CountryCode>" &
        "            <ebiz:CurrencyCode>USD</ebiz:CurrencyCode>" &
        "            <ebiz:ProcessingCommand>Sale</ebiz:ProcessingCommand>" &
        "            <ebiz:SoftwareId>Omegacube</ebiz:SoftwareId>" &
        "            <ebiz:TransactionLookupKey>" & ds1.Rows(0)("INVOICE_NO").ToString() & "</ebiz:TransactionLookupKey>" &
        item_details &
        "            <ebiz:Clerk>" & USER_ID & "</ebiz:Clerk>" &
        "            <ebiz:Terminal>Terminal1</ebiz:Terminal>" &
        "         </ebiz:ePaymentForm>" &
        "      </ebiz:GetEbizWebFormURL>" &
        "   </soapenv:Body>" &
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
