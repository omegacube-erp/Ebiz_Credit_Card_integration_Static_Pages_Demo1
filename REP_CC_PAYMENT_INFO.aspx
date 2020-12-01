<%@ Page Language="VB"  AutoEventWireup="false"  CodeFile="REP_CC_PAYMENT_INFO.aspx.vb" Inherits="_REP_CC_PAYMENT_INFO"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title> Credit Card Payment/Refund Info </title>
<link rel = "SHORTCUT ICON" href="~/images/location1.png" >
<link rel = "ICON" href="~/Images/Cube.ico" type="image/ico"  />
 <link type="text/css" href="Images/Default.css" rel="stylesheet" />
    <link href="App_Themes/default/GridStylesNew.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/default/CSNew.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/default/PivotGridStyles.css" rel="stylesheet" type="text/css" />
<link href="App_Themes/default/main.css" type="text/css" rel="stylesheet" />
<!-- Latest compiled And minified CSS -->
 <link rel = "stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" type="text/css" />
<!-- jQuery library -->
 <Script src = "https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js" type="text/javascript"></script>
<!-- Latest compiled JavaScript -->
 <Script src = "https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" type="text/javascript"></script>
<style type="text/css">
    .dxgvIndentCell {  background-color:white!important;   }
    .dxgvDataRow td.dxgvIndentCell, .dxgvGroupRow td.dxgvIndentCell, .dxgvGroupFooter td.dxgvIndentCell {  border-right: 1 none; border-top: 1 none;}
body, html {padding: 0;margin: 0;}
 .imgbtn {width: 80%;border:   1px solid grey;border-radius:  10px;background-color: white; }
</style>
</head>
<body style="overflow: hidden">
 <script type="text/javascript" language="javascript">
     function pageLoad(sender, args) {
         Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginReq);
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
         if (!args.get_isPartialLoad()) {
             $addHandler(document, "keydown", onKeyDown);
         }
     }
     function beginReq(sender, args) {
         $find('ModalProgress').show();
     }
     function endReq(sender, args) {
         $find('ModalProgress').hide();
     }
function controlEnter (obj, event)
 {     
     var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;     
     if (keyCode == 13)
     {                 
        document.getElementById(obj).focus();
__doPostBack('<%=btnGo.UniqueId%>', "");
        return false;     
     }     
     else  {
        return true;
     }
 }
    </script>
    <form id="form1" runat="server">
    <script type="text/javascript" language="javascript">
        function open_window_doc(s1) { window.open(s1, "DocMangWindow", "location=0,status=0,scrollbars=2,menubar=0,toolbar=0,resizable=1,width=1000,height=700"); return false; }
function onKeyDown(e) {
          if (e && e.keyCode == Sys.UI.Key.esc) {
              var strPop = $get('<%= TextBox9.ClientID %>').value;
              $find(strPop).hide();
          }
      }
        function ShowPopup() {
            $find('mdl').show();
        }
        function ShowPopup1() {
            $find('mdl1').show();
        }
 function Clear_Grid() {
            // get datagrid by rendered table name
            var grid = document.getElementById('<%=dgQuickSearch.ClientID%>');
            var len = grid.rows.length;
            // touch each row, retrieve cell 2 innerHTML
            for (i = 0; i < len; i++) {
                // show raw text for cells, <input type="text"... > for text box
                //alert(grid.rows[i].cells[3].innerHTML);
                var inputelements = grid.rows[i].cells[3].getElementsByTagName('input');
                var maxj = inputelements.length;
                for (j = 0; j < maxj; j++) {
                    if (inputelements[j].getAttribute('type') == 'text') {
                        inputelements[j].value = "";
                    }
                }
            }
        }
 function setTextValueSC() {
            var strVal = $get('<%=hFstrInValues.ClientID%>').value;
            var rblVal = $get('<%=rbl.ClientID%>').value;
            $get($get('<%= hFTextBox1.ClientID %>').value).value = strVal + rblVal;
            $get('<%= hFpnlPopUp1.ClientID %>').style.display = "None";
            $find($get('<%= hFmdlPopup1.ClientID %>').value).hide();
        }
 function setTextValueCT() {
            var rblVal = $get('<%=lstColumns.ClientID%>').value;
            if (rblVal == 'All') {
                document.getElementById('<%=lblColumnname.ClientID%>').innerHTML = '';
                document.getElementById('<%=hdnLblValue.ClientID%>').value = '';
            }
            else {
                document.getElementById('<%=lblColumnname.ClientID%>').innerHTML = rblVal;
                document.getElementById('<%=hdnLblValue.ClientID%>').value = rblVal;
            }
        }
 function showModelPopUp() {
            $find('mdlF').show();
            return false;
        }
 function SetDDDates(dd, fm, td) {
            var ddlReport = document.getElementById(dd);
            var txtFm = document.getElementById(fm);
            var txtTd = document.getElementById(td);

            var Text = ddlReport.options[ddlReport.selectedIndex].text;
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            if (Text == 'Between') {
                document.getElementById(td).style.display = 'block';
                document.getElementById(td).className = 'GridTextBox';
                txtFm.disabled = false;
                txtTd.disabled = false;
            }
            else if (Text == 'Is Null') {
                document.getElementById(td).className = 'hiddencol';
                txtTd.style.display = "";
                txtFm.disabled = true;
            }
            else if (Text == 'Is Not Null') {
                document.getElementById(td).className = 'hiddencol';
                txtTd.style.display = "";
                txtFm.disabled = true;
            }
            else {
                txtFm.disabled = false;
                document.getElementById(td).className = 'hiddencol'
                txtTd.style.display = "";
            }
        }

        function SetDDVar(dt, ex1, ex2) {
            var ddlReport = document.getElementById(dt);
            var txtE1 = document.getElementById(ex1);
            var txtE2 = document.getElementById(ex2);

            var Text = ddlReport.options[ddlReport.selectedIndex].text;
            var Value = ddlReport.options[ddlReport.selectedIndex].value;
            if (Text == 'Between') {
                document.getElementById(ex2).style.display = 'block';
                document.getElementById(ex2).className = 'GridTextBox';
                txtE1.disabled = false;
                txtE2.disabled = false;
            }
            else if (Text == 'Is Null') {
                document.getElementById(ex2).className = 'hiddencol';
                txtE2.style.display = "";
                txtE1.disabled = true;
            }
            else if (Text == 'Is Not Null') {
                document.getElementById(ex2).className = 'hiddencol';
                txtE2.style.display = "";
                txtE1.disabled = true;
            }
            else {
                txtE1.disabled = false;
                document.getElementById(ex2).className = 'hiddencol';
                txtE2.style.display = "";
            }
        }
function ShowAlWindow(dd) {
          var ddlReport = document.getElementById(dd);        
          var Text = ddlReport.options[ddlReport.selectedIndex].text;
          var Value = ddlReport.options[ddlReport.selectedIndex].value;
          
          var n=Value.split("$");
          
          if (n[1] == 'LR' || n[1] == 'CR' || n[1] == 'PG' || n[1] == 'GP' || n[1] == 'CP' ) {
                ReturnValue = window.open(n[0] + ".aspx?Report_ID=REP_CC_PAYMENT_INFO&ALTER_REP_ID=" + n[0], "_blank");
          }
          else if ((n[0] != '') && (n[0].indexOf(".aspx") >= 0 || n[0].indexOf(".ASPX") >= 0 )) {
                ReturnValue = window.open(n[0] + "?Report_ID=REP_CC_PAYMENT_INFO&ALTER_REP_ID=" , "_blank");
          }
          ddlReport.selectedIndex = 0;
      }
function Close_Filter_Grid() {var strPop = $get('<%= TextBox9.ClientID %>').value;if (strPop != '') {$find(strPop).hide();}$find('mdlF').hide();return false;}
    </script>
  <script type="text/javascript">
         function OnInit(s, e) {
             AdjustSize();
         }
         function OnEndCallback(s, e) {
             AdjustSize();
         }
         function OnControlsInitialized(s, e) {
             ASPxClientUtils.AttachEventToElement(window, "resize", function(evt) {
                 AdjustSize();
             });
         }
         function AdjustSize() {
             var topContainer = document.getElementById('topGridContainer');
             var height = Math.max(0, document.documentElement.clientHeight - topContainer.offsetHeight);
             grid.SetHeight(height);
         }
     </script>
<div id="topGridContainer" style="background-color: #CDEAFF;" >
 <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%">
<tr>
<td style="width: 50%; padding: 5px;border-right:1px white solid;" align="center" valign="middle" >
<table width="100%">
<tr align="center" valign="middle" style="border-bottom:1px white solid;">
<td>
<asp:Panel ID="panelLabel11567" runat="server" HorizontalAlign="Left" Width="100%">
<div><asp:Label ID="Label1111" runat="server" Text="Credit Card Payment/Refund Info" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"> 
</asp:Label></div>
<div><asp:Label ID="Label1112" runat="server" Text="" Font-Names="Arial" Font-Bold="True" Font-Size="10pt"> 
</asp:Label></div>
</asp:Panel>
</td>
<td Style="Width:35%">
<div style = "width: 100%;" >
<table width="100%" style="float: right">
<tr>
<td align="center" valign="middle">
<asp:Label ID="lblAlter" runat="server" CssClass="lbfont6" Text="ALT Reports:"></asp:Label>
</td>
<td align="center" valign="middle">
<asp:UpdatePanel ID = "UpdatePanel3" runat="server">
<ContentTemplate>
<asp:DropDownList ID = "ddlAlter" Width="200px" CssClass="cntrlTblSelectBox" runat="server" placeholder="ALT reports"
CausesValidation="False" AutoPostBack="False">
</asp:DropDownList>
</ContentTemplate>
</asp:UpdatePanel>
</td>
</tr>
</table>
</div>
</td>
</tr>
<tr align="center" valign="middle">
<td colspan="2">
<table width="100%">
<tr  style="padding:10px;">
<td align="left" valign="middle">
	<asp:UpdatePanel ID="UpdatePaneMessage" runat="server">
		<ContentTemplate>
			<asp:Label ID="lblMessage" runat="server" Text="Total No Of Records: 0"></asp:Label>
		</ContentTemplate>
	</asp:UpdatePanel>
</td>
<td style = "border-right: 1px solid white;border-left: 1px solid white;" align="center" valign="middle" ><%--Global Search--%>
<div id="GlobalSearch"  runat="server">
<asp:Panel ID="Panel5GlobalSearch" runat="server" DefaultButton="btnGo">
        <table>
            <tr>
                <td>
     <asp:UpdatePanel ID="UpdatePanel8905" runat="server">
     <ContentTemplate>
<span Class="glyphicon glyphicon-triangle-bottom" ID="TextLabel" runat="server"></span>
                    <asp:Label ID="lblColumnname" runat="server" Text="" EnableViewState="true" CssClass="lbfont"></asp:Label>
                    <asp:TextBox ID="txtFltValue" runat="server"></asp:TextBox>
                    <asp:ImageButton ID="btnGo" runat="server" Text="GO" CssClass="btn" ImageUrl="~/Images/Search.png" AlternateText="Refresh" ToolTip="Refresh" style="width:10.5%;background-color:white;padding:5px;border:1px gray solid; border-radius:0 5px 5px 0;vertical-align:top;" />
                    <asp:Panel ID="DPITEM_TYPE" runat="server" Style="display: none; visibility: hidden;">
                        <asp:ListBox ID="lstColumns" Height="100px" CssClass="lbfont2" runat="server" AutoPostBack="false">
                        </asp:ListBox>
                    </asp:Panel>
                    <cc1:DropDownExtender runat="server" ID="ddxITEM_TYPE" TargetControlID="TextLabel"
                        DropDownControlID="DPITEM_TYPE" DropArrowWidth="0" HighlightBackColor="Transparent"
                        HighlightBorderColor="Transparent" DropArrowBackColor="Transparent" />
 </ContentTemplate>
 </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
     <asp:UpdatePanel ID="UpdatePanel555" runat="server">
     <ContentTemplate>
                 <asp:Panel ID="pnldgApply" runat="server" >
                    <asp:DataGrid ID="dgApplyFilter" runat="server" AutoGenerateColumns="False" CssClass="gridRowCls"
                                    AlternatingItemStyle-CssClass="gridAltRowCls" GridLines="None"   Width="100%" ShowHeader="false" 
                                    OnDeleteCommand="DeleteFilter_Item">
                        <HeaderStyle CssClass="detailHdrCls" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
   <img id="Img_Filter4" runat="server" style="cursor: pointer" src='~/Images/Green1.jpg'  onclick="showModelPopUp();return false;" alt="" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateColumn>
                             <asp:BoundColumn DataField="FieldName" HeaderText="Operator1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Operator" HeaderText="Expression1"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Expression" HeaderText="DType"></asp:BoundColumn>
                             <asp:TemplateColumn HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgBtnRowDelete" CommandName="Delete" runat="server" ImageUrl="~/Images/no_filter.gif" />
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                            </asp:TemplateColumn>                           
                        </Columns>
                    </asp:DataGrid>
                    </asp:Panel> 
 </ContentTemplate>
 </asp:UpdatePanel>
                </td>
            </tr>
        </table>
 </asp:Panel>
    </div>
</td>
<td align="center" valign="middle" >
                <div id="divFromTo">
                                <table>
                                    <tr>
                                        <td  align="center" valign="middle">
                                            <b>From</b>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="dateFrom" runat="server" CssClass="GridTextBox" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="dateFrom">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td align="center" valign="middle">
                                            <b>To</b>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:TextBox ID="dateTo" runat="server" CssClass="GridTextBox" Width="100px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="dateTo">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnRefresh1" runat="server" Text="Refresh"  ImageUrl="~/Images/Search.png" AlternateText="Refresh" ToolTip="Refresh" style="width:34%;background-color:white;padding:2px;border:1px gray solid; border-radius:0 5px 5px 0;" />
                                        </td>
                                    </tr>
                                </table>
<div style="display:none;">
                <dx:ASPxGridView ID="dateGrid" runat="server" AutoGenerateColumns="true" ClientInstanceName="dategrid123">
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="FromLabel" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FromDate" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ToLabel" VisibleIndex="3">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ToDate" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter4" runat="server" GridViewID="dateGrid"
                    Landscape="True" PaperKind="A4" />
                </div>
                </div>
</td>
</tr>
</table>
 </td>
</tr>
</table>
</td>
<td style="width: 50%; padding: 5px" align="center" valign="middle">
                                        <cc1:CollapsiblePanelExtender ID="CollapsiblePnlExt" runat="server" TargetControlID="PnlCollapse"
                                            ExpandControlID="pnlImg" CollapseControlID="pnlImg" ImageControlID="Image2"
                                            ExpandedImage="images/blue/openIcon.jpg" CollapsedImage="images/blue/closedIcon.jpg"
                                            SuppressPostBack="true" Enabled="true" Collapsed="false">
                                        </cc1:CollapsiblePanelExtender>
                                        <asp:Panel ID="pnlImg" runat="server" CssClass="collapsePanelHeader" Visible="false" >
                                             <asp:Label ID="lbl9" runat="server" CssClass="dxgv">Click For Options</asp:Label>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="images/blue/closedIcon.jpg" />
                                        </asp:Panel>
                                        <asp:Panel ID="PnlCollapse" runat="server" >
                                            <div id="Expand1">
<table width="100%">
<tr>
<td >
<asp:ImageButton ID="btnPrintS" runat="server" Text="Print Preview" ImageUrl="~/Images/Preview.png" AlternateText="Print Preview" ToolTip="Print Preview" CausesValidation="False" CssClass="imgbtn"  />
</td>
<td >
<div style = "border: 1px solid gray; border-radius: 10px; padding: 10px; background-color: white; width: 90%" >
<table width="100%">
<tr>
<td rowspan="2" style="width: 60%">
<img src="Images/EmailwithText.png" alt="Export" style="width: 100%; border-right: 1px gray solid;"/></td>
<td>
<asp:ImageButton ID="btnEmailXls" runat="server" Text="Email Xls" ImageUrl="~/Images/Excel.png" AlternateText="Email Xls" ToolTip="Email Xls" CausesValidation="False" OnClick="btnExcel_Email_Click" Style="width: 70%; padding: 2px; border-bottom: 1px gray solid;"/>
</td>
</tr>
 <tr>
<td style="width: 40%">
 <asp:DropDownList ID="ddlEmail" Width="100px" CssClass="cntrlTblSelectBox" runat="server" OnSelectedIndexChanged="ddlEmail_SelectedIndexChanged" AutoPostBack="True" EnableViewState="True" Style="display:none;"  >
    <asp:ListItem Text="" Value=""/>
    <asp:ListItem Text=" Pdf" Value=" PDF"/>
    <asp:ListItem Text=" Excel" Value=" XLS"/>
 </asp:DropDownList>
<asp:ImageButton ID = "btnEmailPDF" runat="server" Text="Email pdf" ImageUrl="~/Images/pdf.png" AlternateText="Email pdf" ToolTip="Email PDF" CausesValidation="False" OnClick="btnPDF_Email_Click" Style="width: 70%; padding: 2px; " />
</td>
</tr>
</table>
</div>
</td>
<td Style=" display:none">
<asp:Button ID="btnPrint" runat="server" Text="Print" class='btn'/>
</td>
<td>
<div style = "border: 1px solid gray; border-radius: 10px; padding: 10px; background-color: white; width: 90%" >
<table width="100%">
<tr>
<td rowspan="2" style="width: 60%">
<img src="Images/ExportwithText.png" alt="Export" style="width: 100%;  border-right: 1px gray solid;"/></td>
<td style="width:40%">
<asp:ImageButton ID="btnExcel" runat="server" Text="Export To Excel" ImageUrl="~/Images/Excel.png" AlternateText="Export To Excel" ToolTip="Export To Excel" CausesValidation="False" Style="width: 70%; padding: 2px; border-bottom: 1px gray solid;"/></td>
</tr>
<tr>
<td style="width: 40%">
<asp:ImageButton ID="btnPdfPreview" runat="server" Text="Print" ImageUrl="~/Images/pdf.png" AlternateText="Export To PDF" ToolTip="Export To PDF" CausesValidation="False" Style="width: 70%; padding: 2px;" /></td>
</tr>
</table>
   </div>
</td>
<td>
<asp:ImageButton ID="btnRefresh" runat="server" Text="Refresh" ImageUrl="~/Images/refresh.png" AlternateText="Refresh" ToolTip="Refresh" CausesValidation="false" CssClass="imgbtn"  />
</td>
<td>
<asp:ImageButton ID="btnCustomize" runat="server" Text="Actions" ImageUrl="~/Images/Action.png" AlternateText="Actions" OnClientClick="return false;" ToolTip="Actions" CausesValidation="false" CssClass="imgbtn"  />
<dx:ASPxPopupControl ID = "ASPxPopupControl4" runat="server" PopupElementID="btnCustomize" AllowDragging="True" 
 ClientInstanceName = "ClientPopupControl" Width="400" Height="250" HeaderText="Report Action" CloseOnEscape="true" 
 ShowFooter = "False" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" >
 <ContentCollection>
 <dx:PopupControlContentControl>
<asp:Panel ID="pnlActions" runat="server" >
<asp:Panel runat="Server" ID="Panel3Action" Style="cursor: move; background-color: #E3EDF6;
	border: solid 1px White; color: Black; text-align: center;">
	<asp:Label ID="lblpnlActions" runat="server" CssClass="lbfont6" Text="Actions"></asp:Label>
</asp:Panel>
    <dx:ASPxRoundPanel ID="ASPxRoundPanel2" runat="server" ShowHeader="False" Width="100%" >
	<PanelCollection>
	    <dx:PanelContent ID="PanelContent1" runat="server">
<asp:Panel ID="Panel4pnlActions" runat="server" DefaultButton="btnApply">
		<table>
		    <tr>
			<td>
 <table class="displaySelectedFieldsTbl" border='0' style='display: block' cellspacing='0'
 cellpadding='0'>
 <tr>
  <td class="GridHeader" align="center" valign="middle">
 Group By
  </td>
<td class="GridItem" align="center" valign="middle">
 <asp:DropDownList ID="ddlGroupBy" runat="server" Width="160px" CssClass="cntrlTblSelectBox">
<asp:ListItem Text="TRANS_DATE" Value="0"></asp:ListItem>
<asp:ListItem Text="None" Value="1"></asp:ListItem>
</asp:DropDownList>
 </td>
</tr>
 <tr>
  <td class="GridHeader" align="center" valign="middle">
 Expand All
 </td>
<td class="GridItem" align="center" valign="middle">
<asp:CheckBox ID="chkAll" runat="server" Checked="true" />
</td>
</tr>
<tr>
 <td class="GridHeader" align="center" valign="middle">
 Group footer mode
 </td>
<td class="GridItem" align="center" valign="middle">
<asp:DropDownList ID="ddlGroupFooter" runat="server" Width="160px" CssClass="cntrlTblSelectBox">
 </asp:DropDownList>
 </td>
</tr>
 <tr>
 <td class="GridHeader" align="center" valign="middle">
Rows Per Page
</td>
 <td class="GridItem" align="center" valign="middle">
<asp:DropDownList ID="ddlGridPages" runat="server" Width="160px" CssClass="cntrlTblSelectBox">
 <asp:ListItem Text="10" Value="10" ></asp:ListItem>
<asp:ListItem Text="20" Value="20"></asp:ListItem>
 <asp:ListItem Text="30" Value="30"></asp:ListItem>
<asp:ListItem Text="50" Value="50"></asp:ListItem>
<asp:ListItem Text="100" Value="100"></asp:ListItem>
<asp:ListItem Text="200" Value="200"></asp:ListItem>
<asp:ListItem Text="500" Value="500"></asp:ListItem>
<asp:ListItem Text="1000" Value="1000" ></asp:ListItem>
<asp:ListItem Text="2000" Value="2000"></asp:ListItem>
<asp:ListItem Text="5000" Value="5000"></asp:ListItem>
<asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
</asp:DropDownList>
</td>
</tr>
<tr>
<td class="GridHeader" align="center" valign="middle">
Print Format
</td>
<td class="GridItem" align="center" valign="middle">
<asp:RadioButtonList ID="rblPrintFormat" runat="server">
    <asp:ListItem Text="Potrait" Value="Potrait" Selected="True"></asp:ListItem>
    <asp:ListItem Text="Landscape" Value="Landscape"></asp:ListItem>
</asp:RadioButtonList>
</td>
</tr>
</table>
			</td>
		    </tr>
		    <tr>
			<td class="GridItem" align="center" valign="middle">
			    <asp:Button ID="btnApply" runat="server" Text="Apply" class='btn' />
			</td>
		    </tr>
		</table>
 </asp:Panel>
	    </dx:PanelContent>
	</PanelCollection>
    </dx:ASPxRoundPanel>
</asp:Panel>
</dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
</td>
<td>
<asp:ImageButton ID="btnFilter" runat="server" Text="Filter" ImageUrl="~/Images/Filter.png" AlternateText="Filter" OnClientClick="return false;" ToolTip="Filter" CausesValidation="False" CssClass="imgbtn"  />
<dx:ASPxPopupControl ID = "ASPxPopupControl2" runat="server" PopupElementID="btnFilter" AllowDragging="True" 
 ClientInstanceName = "ClientPopupControl" Width="500" Height="350" HeaderText="Report Filter" CloseOnEscape="true" 
 ShowFooter = "False" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" >
 <ContentCollection>
 <dx:PopupControlContentControl>
 <asp:Panel ID="pnlFilter" runat="server" >
<asp:Panel runat="Server" ID="Panel3Filter" Style="cursor: move; background-color: #E3EDF6;
	border: solid 1px White; color: Black; text-align: center;">
	<asp:Label ID="lblpnlFilter" runat="server" CssClass="lbfont6" Text="Filter"></asp:Label>
</asp:Panel>
<dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" ShowHeader="False" Width="100%">
<PanelCollection>
<dx:PanelContent ID="PanelContent2" runat="server">
<asp:Panel ID="Panel3pnlFilter" runat="server" DefaultButton="btnFltApply">
<table>
<tr>
<td>
<asp:Panel ID="pnlNoSearch" runat="server" CssClass="collapsePanel" Visible="false">
<table class="cntrTbl" cellspacing="0" cellpadding="0" border="0" width="100%">
<tr align="center" valign="middle" >
    <td class="GridHeader" align="center" valign="middle" >
	<asp:Label ID="lblFLabel" runat="server" Text="FieldLabel"></asp:Label>
    </td>
    <td class="GridHeader" align="center" valign="middle">
	<asp:Label ID="Label1" runat="server" Text="FieldName" ></asp:Label>
    </td>
    <td class="GridHeader" align="center" valign="middle" >
	<asp:Label ID="Label2" runat="server" Text="Operators" ></asp:Label>
    </td>
    <td class="GridHeader" align="center" valign="middle">
	<asp:Label ID="Label3" runat="server" Text="Expression" ></asp:Label>
    </td>
</tr>
<tr align="center" valign="middle">
    <td class="GridItem" align="center" valign="middle" colspan="4">
	<asp:Label ID="Label4" runat="server" Text="No Search Defined" Width="120px"></asp:Label>
    </td>
    <td>
    </td>
    <td>
    </td>
    <td>
    </td>
</tr>
</table>
</asp:Panel>
<table class="cntrTbl" cellspacing="0" cellpadding="0" border="0" width="100%">
		     <tr>
			 <td>
			     <table width="100%" class="cntrTbl" cellpadding="1" cellspacing="2">
				 <tr runat="server" id="tblSearch">
				     <td runat="server">
					 <table class="displaySelectedFieldsTbl" cellpadding="0" cellspacing="0">
					     <tr>
						 <td>
<asp:Panel ID="PnlContent" runat="server" CssClass="collapsePanel" Height="200px" ScrollBars="Auto">
<div id="qPrint">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" OnUnload="UpdatePanel1_Unload">
<ContentTemplate>
<asp:DataGrid ID="dgQuickSearch" runat="server" AutoGenerateColumns="False" CssClass="displaySelectedFieldsTbl">
<HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
<ItemStyle CssClass="GridItem" />
<AlternatingItemStyle CssClass="GridAltItem " />
<Columns>
<asp:TemplateColumn HeaderText="FieldLabel">
<ItemTemplate>
<asp:Label ID="lblFLabel" runat="server" CssClass="displaySelectedFieldsCell"></asp:Label>
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="FieldName">
<ItemTemplate>
<asp:Label ID="lblFName" runat="server" CssClass="displaySelectedFieldsCell"></asp:Label>
</ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Operators">
<ItemTemplate>
<asp:DropDownList ID="ddlDrop" Width="100px" CssClass="cntrlTblSelectBox" runat="server"
AutoPostBack="false" CausesValidation="false">
</asp:DropDownList>
 <asp:DropDownList ID="ddlDate" Width="100px" CssClass="cntrlTblSelectBox" runat="server"
AutoPostBack="false" CausesValidation="false">
 </asp:DropDownList>
 </ItemTemplate>
 </asp:TemplateColumn>
 <asp:TemplateColumn HeaderText="Expression">
<ItemTemplate>
<table>
    <tr>
	<td>
	    <asp:TextBox ID="expression" runat="server" Width="100px" CssClass="GridTextBox"></asp:TextBox>
<dx:ASPxDateEdit ID="frmDate" runat="server"  EditFormatString="MM/dd/yyyy" Width="100px" AllowUserInput="False" HelpTextSettings-DisplayMode="Inline" >   
</dx:ASPxDateEdit>
	</td>
	<td>
	    <asp:TextBox ID="expression2" runat="server" Width="100px" CssClass="GridTextBox"></asp:TextBox>
<dx:ASPxDateEdit ID="toDate" runat="server"  EditFormatString="MM/dd/yyyy" AllowUserInput="False" Width="100px" HelpTextSettings-DisplayMode="Inline"  >
</dx:ASPxDateEdit> 
	</td>
    </tr>
</table>
 </ItemTemplate>
 <HeaderStyle Width="215px" />
 </asp:TemplateColumn>
  <asp:BoundColumn DataField="FIELD_TYPE" HeaderText="Stype"></asp:BoundColumn>
<asp:TemplateColumn>
<ItemTemplate>
    <asp:ImageButton ID="imgBtn1" runat="server" ImageUrl="images/LOV.gif" OnClick="btnShowPopup_Click" />
    <asp:Button ID="Button2" runat="server" Text="" Style="display: none" />
    <cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="Button2" PopupControlID="pnlPopup1" />
</ItemTemplate>
</asp:TemplateColumn>
   </Columns>
 </asp:DataGrid>
 </ContentTemplate>
 </asp:UpdatePanel>
 </div>
</asp:Panel>
						 </td>
					     </tr>
					 </table>
				     </td>
				 </tr>
				 <tr>
				     <td>
				     </td>
				 </tr>
				 <tr>
				     <td valign="middle" align="center" class="quickSearchTitle">
					 <input id="btnClear" type="button" value="Clear" onclick="Clear_Grid()" class='btn'
					     runat="server" />
				     </td>
				 </tr>
				 <tr>
				     <td>
					 <div style="display: none">
					     <asp:HiddenField ID="RID" runat="server" Value="REP_CC_PAYMENT_INFO" />
					 </div>
				     </td>
				 </tr>
				 <tr>
				     <td>
					 <table>
					     <tr>
						 <td>
						     <asp:UpdateProgress ID="UpdateProgress2" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
							 <ProgressTemplate>
							     <table>
								 <tr>
								     <td align="center" valign="middle">
									 Update in Progress..
								     </td>
								 </tr>
								 <tr>
								     <td align="center" valign="middle">
									 <img src="Images/ajax-loader_bf.gif" />
								     </td>
								 </tr>
							     </table>
							 </ProgressTemplate>
						     </asp:UpdateProgress>
						 </td>
					     </tr>
					 </table>
				     </td>
				 </tr>
			     </table>
			 </td>
		     </tr>
		</table>
    </td>
</tr>
<tr>
    <td class="GridItem" align="center" valign="middle">
	<asp:Button ID="btnFltApply" runat="server" Text="Apply" class='btn' />
    </td>
</tr>
</table>
 </asp:Panel>
</dx:PanelContent>
</PanelCollection>
</dx:ASPxRoundPanel>
</asp:Panel>
</dx:PopupControlContentControl>
</ContentCollection>
</dx:ASPxPopupControl>
</td>
<td>
<asp:ImageButton ID="btnSort" runat="server" Text="Sort" ImageUrl="~/Images/Sort.png" AlternateText="Sort" OnClientClick="return false;" ToolTip="Sort" CausesValidation="false" CssClass="imgbtn"  />
<dx:ASPxPopupControl ID = "ASPxPopupControl3" runat="server" PopupElementID="btnSort" AllowDragging="True" 
ClientInstanceName = "ClientPopupControl" Width="500" Height="350" HeaderText="Report Sort" CloseOnEscape="true" 
ShowFooter = "False" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton">
<ContentCollection>
<dx:PopupControlContentControl>
 <asp:Panel ID="pnlSort" runat="server" >
<asp:Panel runat="Server" ID="Panel3Sort" Style="cursor: move; background-color: #E3EDF6;
	border: solid 1px White; color: Black; text-align: center;">
	<asp:Label ID="lblpnlSort" runat="server" CssClass="lbfont6" Text="Sort"></asp:Label>
</asp:Panel>
<dx:ASPxRoundPanel ID="ASPxRoundPanel3" runat="server" ShowHeader="False" Width="100%" >
<PanelCollection>
<dx:PanelContent ID="PanelContent3" runat="server">
<asp:Panel ID="PanelSortButton" runat="server" DefaultButton="btnSortApply">
<table>
<tr>
<td>
<table class="cntrTbl" cellspacing="0" cellpadding="0" border="0">
    <tr>
	<td>
	    <table width="100%" class="cntrTbl" cellpadding="1" cellspacing="2">
		<tr runat="server" id="Tr1">
		    <td>
			<asp:UpdatePanel ID="UpdatePanelSortNew" runat="server" OnUnload="UpdatePanel1_Unload">
			    <ContentTemplate>
				<table class="displaySelectedFieldsTbl" cellpadding="0" cellspacing="0">
				    <tr>
					<td align="left" colspan="3">
					    <table width="100%" cellspacing="0" cellpadding="0" border="0">
						<tr>
						    <td class="cntrTblHdrCell" width="10%">
							<img alt="" src="images/sort_32.gif" />
						    </td>
						    <td class="cntrTblHdrCell" nowrap="nowrap" width="60%">
							Sort
						    </td>
						    <td nowrap="nowrap" width="30%">
						    </td>
						</tr>
						<tr>
						    <td>
						    </td>
						    <td>
						    </td>
						    <td>
						    </td>
						</tr>
					    </table>
					</td>
				    </tr>
				    <tr>
					<td valign="top" align="left">
					    <asp:ListBox ID="lstSort" runat="server" CssClass="cntrlTblSelectBox" Width="140px"
						Height="185px" SelectionMode="Multiple" BorderStyle="Outset"></asp:ListBox>
					</td>
					<td class="addFieldsToRightIcon" valign="middle">
					    <asp:ImageButton ID="imgAddSort" runat="server" ImageUrl="images/blue/right_arrow.gif" />
					</td>
					<td valign="top" align="left">
					    <asp:Panel ID="pnlContentSort" runat="server" CssClass="collapsePanel">
						<div id="Div3">
						    <asp:DataGrid ID="dgSort" runat="server" AutoGenerateColumns="False" CssClass="displaySelectedFieldsTbl"
							OnDeleteCommand="DeleteSort_Item">
							<HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" VerticalAlign="Middle" />
							<ItemStyle CssClass="GridItem" />
							<AlternatingItemStyle CssClass="GridAltItem " />
							<Columns>
							    <asp:TemplateColumn HeaderText="x">
								<ItemTemplate>
								    <asp:ImageButton ID="imgBtnColDelete" CommandName="Delete" runat="server" ImageUrl="images/blue/left_arrow.gif" />
								</ItemTemplate>
								<HeaderStyle Font-Bold="True" />
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="FieldName">
								<ItemTemplate>
								    <asp:Label ID="lblFName" runat="server" CssClass="displaySelectedFieldsCell"></asp:Label>
								</ItemTemplate>
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Direction">
								<ItemTemplate>
								    <asp:DropDownList ID="ddlSort" Width="100px" CssClass="cntrlTblSelectBox" runat="server">
									<asp:ListItem Text="Ascending" Value="Ascending" />
									<asp:ListItem Text="Descending" Value="Descending" />
								    </asp:DropDownList>
								</ItemTemplate>
								<HeaderStyle Width="100px" />
							    </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="Sort Seq">
								<ItemTemplate>
								    <asp:ImageButton runat="server" ImageUrl="~/Images/shuttle_up.png" ID="btnUp" CommandName="MoveUp" />
								    <asp:ImageButton runat="server" ImageUrl="~/Images/shuttle_down.png" ID="btnDown"
									CommandName="MoveDown" />
								</ItemTemplate>
								<HeaderStyle Font-Bold="True" />
								<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="false" />
							    </asp:TemplateColumn>
							</Columns>
						    </asp:DataGrid>
						</div>
					    </asp:Panel>
					</td>
					<td valign="top" align="left">
					    <asp:Panel ID="pnlNoSort" runat="server" CssClass="collapsePanel" Visible="false">
						<table class="cntrTbl" cellspacing="0" cellpadding="0" border="0" width="100%">
						    <tr align="center" valign="middle">
							<td class="GridHeader" align="center" valign="middle">
							    <asp:Label ID="Label5" runat="server" Text="FieldName"></asp:Label>
							</td>
							<td class="GridHeader" align="center" valign="middle">
							    <asp:Label ID="Label7" runat="server" Text="Direction"></asp:Label>
							</td>
							<td class="GridHeader" align="center" valign="middle">
							    <asp:Label ID="Label8" runat="server" Text="Sort Seq"></asp:Label>
							</td>
						    </tr>
						    <tr align="center" valign="middle">
							<td class="GridItem" align="center" valign="middle" colspan="4">
							    <asp:Label ID="Label10" runat="server" Text="No Sort Defined" Width="120"></asp:Label>
							</td>
							<td>
							</td>
							<td>
							</td>
							<td>
							</td>
						    </tr>
						</table>
					    </asp:Panel>
					</td>
				    </tr>
				</table>
			    </ContentTemplate>
			</asp:UpdatePanel>
		    </td>
		</tr>
		<tr>
		    <td>
		    </td>
		</tr>
		<tr>
		    <td>
			<table>
			    <tr>
				<td>
				    <asp:UpdateProgress ID="UpdateProgress12236" AssociatedUpdatePanelID="UpdatePanelSortNew"
					runat="server">
					<ProgressTemplate>
					    <table>
						<tr>
						    <td align="center" valign="middle">
							Update in Progress..
						    </td>
						</tr>
						<tr>
						    <td align="center" valign="middle">
							<img src="Images/ajax-loader_bf.gif" />
						    </td>
						</tr>
					    </table>
					</ProgressTemplate>
				    </asp:UpdateProgress>
				</td>
			    </tr>
			</table>
		    </td>
		</tr>
	    </table>
	</td>
    </tr>
</table>
</td>
</tr>
<tr>
<td class="GridItem" align="center" valign="middle">
<asp:Button ID="btnSortApply" runat="server" Text="Apply" class='btn' />
</td>
</tr>
</table>
</asp:Panel>
</dx:PanelContent>
</PanelCollection>
</dx:ASPxRoundPanel>
</asp:Panel>
</dx:PopupControlContentControl>
 </ContentCollection>
</dx:ASPxPopupControl>
</td>
<td style='display:none;' >
<asp:Button ID="btnChart" runat="server" Text="Chart" class='btn'  CausesValidation="false" />    
</td>
<td style='display:none;' >
<asp:Button ID="btnGroupBy" runat="server" Text="Group Summary" class='btn' CausesValidation="false"  />
</td>
<td style='display:none;' >
<asp:Button ID="btnCtrBreak" runat="server" Text="Control Break" class='btn' CausesValidation="false" />
</td>
<td style='display:none;'>
<asp:Button ID="btnPvtReport" runat="server" Text="Pivot Report" class='btn' CausesValidation="false" />
</td>
<td>
<asp:ImageButton ID="btnWizard" runat="server" Text="Wizard" ImageUrl="~/Images/wizard.png" AlternateText="Wizard" OnClientClick="Return False;" ToolTip="Wizard" CausesValidation="false" CssClass="imgbtn"  />
 <dx:ASPxPopupControl ID="PopupControl" runat="server" PopupElementID="btnWizard" AllowDragging="False"
ClientInstanceName="ClientPopupControl" Width="500" Height="350" HeaderText="Report Wizard"
ShowFooter="False" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" CloseAction="CloseButton" >  
</dx:ASPxPopupControl>
</td>
 <td> 
<asp:ImageButton ID="btnEdit" runat="server" Text="Edit Report" ImageUrl="~/Images/edit-icon.png" AlternateText="Edit" ToolTip="Edit Report" CausesValidation="false" CssClass="imgbtn" />
</td>
</tr>
</table>
</div>
</asp:Panel>
</td>
</tr>
</table>
<div id="divViewNames" runat="server" visible="false">
<table width="100%">
<tr>
<td class="cntrTblHdrCell">
<asp:Panel ID="PnlTitle" runat="server" CssClass="collapsePanelHeader">
    <asp:Image ID="Image1" runat="server" ImageUrl="images/blue/closedIcon.jpg" />
</asp:Panel>
</td>
</tr>
<tr>
    <td align="left" valign="middle">
	<asp:Panel ID="Panel2" runat="server" CssClass="collapsePanel">
	    <div id="Expand">
	      <table>
	                <tr>
	        <td>
		<dx:ASPxGridView ID="searchCriteria" runat="server" AutoGenerateColumns="true" ClientInstanceName="sgrid122"   Visible="false" >
		    <Columns>
			<dx:GridViewDataColumn Caption="Type" VisibleIndex="0">
			    <DataItemTemplate>
				<img id="Img1" runat="server" src='<%#GetImageName(Eval("FltType"),Eval("FltAply"))%>' />
			    </DataItemTemplate>
			</dx:GridViewDataColumn>
			<dx:GridViewDataTextColumn FieldName="FieldName" VisibleIndex="1">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="Operator" VisibleIndex="2">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="Expression" VisibleIndex="3">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataColumn Caption="Applied" VisibleIndex="4" FieldName="FltAply" Visible="false">
			    <DataItemTemplate>
				<img id="Img1" runat="server" src='<%#GetImageName(Eval("FltType"),Eval("FltAply"))%>' />
			    </DataItemTemplate>
			</dx:GridViewDataColumn>
			<dx:GridViewDataTextColumn FieldName="FltColor" VisibleIndex="5" Visible="false">
			</dx:GridViewDataTextColumn>
		    </Columns>
		    <SettingsPager Mode="ShowAllRecords" />
		</dx:ASPxGridView>
		   <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="searchCriteria"
	Landscape="True" PaperKind="A4" />                                
	        </td>
<td align="right" valign="top">
    <dx:ASPxGridView ID="sortCriteria" runat="server" AutoGenerateColumns="false" ClientInstanceName="sgridasddsfds">
	<Columns>
	    <dx:GridViewDataColumn Caption="Type" VisibleIndex="0">
		<DataItemTemplate>
		    <img id="Img3" runat="server" src='<%#GetImageName(Eval("FltType"),Eval("FltAply"))%>' />
		</DataItemTemplate>
	    </dx:GridViewDataColumn>
	    <dx:GridViewDataTextColumn FieldName="FieldName" VisibleIndex="1">
	    </dx:GridViewDataTextColumn>
	    <dx:GridViewDataTextColumn FieldName="SortOrder" VisibleIndex="2">
	    </dx:GridViewDataTextColumn>
	    <dx:GridViewDataTextColumn FieldName="SortIndex" VisibleIndex="3">
	    </dx:GridViewDataTextColumn>
	</Columns>
	<SettingsPager Mode="ShowAllRecords" />
    </dx:ASPxGridView>
</td>
</tr>
</table>
	    </div>
	</asp:Panel>
    </td>
</tr>
</table>
<cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" runat="server" TargetControlID="Panel2"
ExpandControlID="PnlTitle" CollapseControlID="PnlTitle" ImageControlID="Image1"
ExpandedImage="images/blue/openIcon.jpg" CollapsedImage="images/blue/closedIcon.jpg"
SuppressPostBack="true" Enabled="true" Collapsed="true">
</cc1:CollapsiblePanelExtender>
</div>
 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Style="display: none">
                    <dx:ASPxGridView ID="sgrid" runat="server" AutoGenerateColumns="true" ClientInstanceName="sgrid123">
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="sgrid"
                        Landscape="True" PaperKind="A4" />
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
</div>
 <div id="bottomGridContainer">
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
<ContentTemplate>
<div id="divData" runat="server">
<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid" SettingsPager-AlwaysShowPager="True" Width="100%" >
<Templates>
<GroupRowContent>
<asp:Label ID="Label1" runat="server" Text="<%# GetLabelText(Container)%>" Visible="<%# GetLabelVisible(Container)%>"></asp:Label>
</GroupRowContent>
</Templates>
<Columns>
<dx:GridViewDataTextColumn FieldName="DOC_TYPE" Caption="Doc Type" VisibleIndex="1"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="USER_ID" Caption="User Id" VisibleIndex="10"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataDateColumn FieldName="CREATED_DATE" Caption="Created Date" VisibleIndex="11"  Width=""  >
<PropertiesDateEdit EnableDefaultAppearance="true" AllowUserInput="false" EditFormat="Custom" DisplayFormatString="MM/dd/yyyy" EditFormatString="MM/dd/yyyy" AllowNull="true" />
</dx:GridViewDataDateColumn>
<dx:GridViewDataTextColumn FieldName="CUSTTOKEN" Caption="Custtoken" VisibleIndex="12"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="PMTOKEN" Caption="Pmtoken" VisibleIndex="13"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TRANRESULTCODE" Caption="Tranresultcode" VisibleIndex="14"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TRANREFNUM" Caption="Tranrefnum" VisibleIndex="15"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="MASKEDCC" Caption="Maskedcc" VisibleIndex="16"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="CCTYPE" Caption="Cctype" VisibleIndex="17"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="PAYBYTYPE" Caption="Paybytype" VisibleIndex="18"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="DOC_NO" Caption="Doc No" VisibleIndex="2"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AMOUNT" Caption="Amount" VisibleIndex="3"  Width=""  >
<PropertiesTextEdit EncodeHtml="False" DisplayFormatString="N2" />
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="RESPONSE" Caption="Response" VisibleIndex="4"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TRANS_DATE" Caption="Trans Date" VisibleIndex="5"  Width=""  >
<PropertiesTextEdit EncodeHtml="False" DisplayFormatString="MM/dd/yyyy" />
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="API_TOKEN" Caption="Api Token" VisibleIndex="6"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="CUSTOMER_NO" Caption="Customer No" VisibleIndex="7"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="NAME" Caption="Name" VisibleIndex="8"  Width=""  >
</dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="STATUS" Caption="Status" VisibleIndex="9"  Width=""  >
</dx:GridViewDataTextColumn>
</Columns>
<Settings ShowGroupFooter="VisibleAlways" ShowGroupButtons="true" ShowFooter="true" ShowVerticalScrollBar="true" />
<ClientSideEvents Init="OnInit" EndCallback="OnEndCallback" />
<TotalSummary>
<dx:ASPxSummaryItem FieldName="AMOUNT" SummaryType="Sum" DisplayFormat="N2" />
</TotalSummary>
<GroupSummary>
<dx:ASPxSummaryItem FieldName="AMOUNT" ShowInGroupFooterColumn="AMOUNT" SummaryType="Sum" DisplayFormat="N2" />
</GroupSummary>
 <Styles>
    <Header BackColor="Transparent" Font-Bold="true"> </Header>
    <GroupPanel BackColor="Transparent"></GroupPanel>
    <GroupRow BackColor ="Transparent"></GroupRow>
    <GroupFooter BackColor="Transparent" HorizontalAlign="Right" VerticalAlign="Middle"> </GroupFooter>
    <Footer BackColor="Transparent" HorizontalAlign="Right" VerticalAlign="Middle"> </Footer>
 </Styles>
</dx:ASPxGridView>
<dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="ASPxGridView1" Landscape="True" PaperKind="A4" />
<div id="updateProgressDiv" style="display: none; height: 40px; width: 40px">
<img src="Images/ajax-loader_bf.gif" />
</div>
</div>
</ContentTemplate>
</asp:UpdatePanel>
<asp:Button ID="Button2" runat="server" Text="" Style="display: none" />
<cc1:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnCustomize" PopupControlID="pnlActions" BackgroundCssClass="modalBackground" RepositionMode="None" PopupDragHandleControlID="Panel3Action" BehaviorID="mdl" />
<cc1:ModalPopupExtender ID="mdlFilter" runat="server" TargetControlID="btnFilter" PopupControlID="pnlFilter" BackgroundCssClass="modalBackground" RepositionMode="None" PopupDragHandleControlID="Panel3Filter" BehaviorID="mdlF" />
<cc1:ModalPopupExtender ID="mdlSort" runat="server" TargetControlID="btnSort" PopupControlID="pnlSort" BackgroundCssClass="modalBackground" RepositionMode="None" PopupDragHandleControlID="Panel3Sort" BehaviorID="mdlS" />
</div>
 <div id="rbGroup">
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="searchID" runat="server" />
                <asp:HiddenField runat="server" ID="hdnTab1" Value="0" />
                <asp:HiddenField runat="server" ID="hFTextBox1" Value="" />
                <asp:HiddenField runat="server" ID="hFstrInValues" Value="" />
                <asp:HiddenField runat="server" ID="hFpnlPopUp1" Value="" />
                <asp:HiddenField runat="server" ID="hFmdlPopup1" Value="" />
               <asp:HiddenField runat="server" ID="hdnViewName" Value="V_CC_PAYMENT_INFO" />
                <asp:HiddenField runat="server" ID="hdnLblValue" Value="" />
                <asp:TextBox ID="TextBox9" runat="server" Width="100px" Style="display: none"></asp:TextBox>
                <asp:Panel ID="pnlPopUp1" runat="server" BorderWidth="1" ScrollBars="Auto" CssClass="dhtmlSubMenuC"
                    Style="display: none">
                    <asp:Panel ID="Panel7" runat="server">
                        <asp:Label ID="Label6" CssClass="displaySelectedFieldsCell" runat="server" Text="Press ESC Key to Close"></asp:Label>
                    </asp:Panel>
                    <asp:Panel ID="Panel8" runat="server" ScrollBars="Auto">
                        <asp:ListBox ID="rbl" runat="server" Width="180px" Height="180px" CssClass="dhtmlSubMenuB">
                        </asp:ListBox>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
<cc1:ModalPopupExtender ID="ModalProgress" BehaviorID="ModalProgress" runat="server"
        TargetControlID="panelUpdateProgress" PopupControlID="panelUpdateProgress" Enabled="True">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="panelUpdateProgress" runat="server" CssClass="updateProgress">
        <asp:UpdateProgress ID="UpdateProg1" DynamicLayout="true" runat="server">
            <ProgressTemplate>
                <div>
                    <table>
                        <tr align="center" valign="middle">
                            <td>
                                <img src="Images/ajax-loader_bf.gif" alt="Processing" />
                            </td>
                        </tr>
                        <tr align="center" valign="middle">
                            <td>
                                <div class="messageCellCls">
                                    Processing ...
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </asp:Panel>
<dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" ClientInstanceName="popupControl"
     Height="600px" Modal="True" Width="700px" AllowDragging="True" PopupHorizontalAlign="WindowCenter"
     PopupVerticalAlign="WindowCenter" ContentUrl="email_Popup.aspx"  HeaderText="Email This Report">
    <HeaderStyle Paddings-Padding="10px" Font-Size="Large" />
</dx:ASPxPopupControl>
</form>
</body>
</html>
