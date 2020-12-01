<%@ Page Language="VB" AutoEventWireup="false" CodeFile="REP_CC_PAYMENT_INFO_Print.aspx.vb"   Inherits="_REP_CC_PAYMENT_INFO_Print" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <title>Credit Card Payment/Refund Info Print </title>
<link rel = "SHORTCUT ICON" href="~/images/location1.png" >
<link rel = "ICON" href="~/Images/Cube.ico" type="image/ico"  />
</head>
<body>
    <form id="form1" runat="server">
    <div>
<asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
           <ContentTemplate>
               <asp:Panel ID="Panel1" runat="server" Style="display: none" >
                   <dx:ASPxGridView ID="sgrid" runat="server" AutoGenerateColumns="true" ClientInstanceName="sgrid123"  Settings-ShowColumnHeaders="false">
<Columns>
<dx:GridViewDataTextColumn FieldName="FieldName" VisibleIndex="1">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Operator" VisibleIndex="2">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Expression" VisibleIndex="3">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="FltAply" VisibleIndex="4" Visible="false">
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="FltColor" VisibleIndex="5" Visible="false">
</dx:GridViewDataTextColumn>
</Columns>
                   </dx:ASPxGridView>
                   <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="sgrid"
                       Landscape="True" PaperKind="A4" />
 <dx:ASPxGridView ID="dateGrid" runat="server" AutoGenerateColumns="true" ClientInstanceName="dategrid123" >
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="FromLabel" VisibleIndex="1" >
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FromDate" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ToLabel" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ToDate" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                        </Columns>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="dateGrid" 
                        Landscape="True" PaperKind="A4" />
               </asp:Panel>
           </ContentTemplate>
       </asp:UpdatePanel>
                                                                <div style="display: none">
                                                                    <asp:HiddenField ID="RID" runat="server" Value="REP_CC_PAYMENT_INFO" />
                                                                </div>
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid">
<Columns>
<dx:GridViewDataTextColumn FieldName="DOC_TYPE" Caption="Doc Type" VisibleIndex="1"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="USER_ID" Caption="User Id" VisibleIndex="10"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataDateColumn FieldName="CREATED_DATE" Caption="Created Date" VisibleIndex="11"  ExportWidth="100"  >
<PropertiesDateEdit EnableDefaultAppearance="true" AllowUserInput="false" EditFormat="Custom" DisplayFormatString="MM/dd/yyyy" EditFormatString="MM/dd/yyyy" AllowNull="true" />
</dx:GridViewDataDateColumn>
<dx:GridViewDataTextColumn FieldName="CUSTTOKEN" Caption="Custtoken" VisibleIndex="12"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="PMTOKEN" Caption="Pmtoken" VisibleIndex="13"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TRANRESULTCODE" Caption="Tranresultcode" VisibleIndex="14"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TRANREFNUM" Caption="Tranrefnum" VisibleIndex="15"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="MASKEDCC" Caption="Maskedcc" VisibleIndex="16"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="CCTYPE" Caption="Cctype" VisibleIndex="17"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="PAYBYTYPE" Caption="Paybytype" VisibleIndex="18"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="DOC_NO" Caption="Doc No" VisibleIndex="2"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AMOUNT" Caption="Amount" VisibleIndex="3"  ExportWidth="100"  >
<PropertiesTextEdit EncodeHtml="False" DisplayFormatString="N2" />
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="RESPONSE" Caption="Response" VisibleIndex="4"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TRANS_DATE" Caption="Trans Date" VisibleIndex="5"  ExportWidth="100"  >
<PropertiesTextEdit EncodeHtml="False" DisplayFormatString="MM/dd/yyyy" />
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="API_TOKEN" Caption="Api Token" VisibleIndex="6"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="CUSTOMER_NO" Caption="Customer No" VisibleIndex="7"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="NAME" Caption="Name" VisibleIndex="8"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="STATUS" Caption="Status" VisibleIndex="9"  ExportWidth="100"  >
</dx:GridViewDataTextColumn>
</Columns>
<Settings ShowGroupFooter="VisibleAlways" ShowGroupButtons="true" ShowFooter="true" />
<TotalSummary>
<dx:ASPxSummaryItem FieldName="AMOUNT" SummaryType="Sum" DisplayFormat="N2" />
</TotalSummary>
<GroupSummary>
<dx:ASPxSummaryItem FieldName="AMOUNT" ShowInGroupFooterColumn="AMOUNT" SummaryType="Sum" DisplayFormat="N2" />
</GroupSummary>
</dx:ASPxGridView>
<dx:aspxgridviewexporter id="ASPxGridViewExporter1" runat="server" gridviewid="ASPxGridView1"   landscape="True" paperkind="A4"/>
</div>
</form>
</body>
</html>
