<%@ Page Language="VB"  AutoEventWireup="false"  CodeFile="CC_INVOICE_LOV.aspx.vb" Inherits="_CC_INVOICE_LOV"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Joel.Net.Refresh" Namespace="Joel.Net" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
<title> CC_INVOICE_LOV </title>
<link href="App_Themes/default/main.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" language="javascript">
function ShowDetails(n)
{
window.returnValue =n; 
window.close();
}
function ShowDetails1(cc, n)
{
 window.opener.updateParent(cc, n); 
window.close();
}
</script>
<body>
<form id="form1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:Label id="Row_count_message" runat="server" Visible="False" CssClass="lbfont6" Text=""></asp:Label>
<Table widht="100%">
<tr>
<td>
<table>
<tr>
<td>
<b>	<asp:Label id="L_key_search_fill" runat="server" CssClass="lbfont6" Text="Keyword Search"></asp:Label></b>
</td>
<td>
<asp:TextBox id="TxtFilterName1" runat="server" AutoPostBack="false" CssClass="lbfont2"></asp:TextBox>
</td>
<td>
<asp:Button runat="server" ID="keyword_go" CssClass="btn" Text="GO" />
</td>
</tr>
<tr>
<td>
<asp:DropDownList ID="field_search_k" runat="server" CssClass="lbfont3" Width="120px" >
			  <asp:ListItem Text="" Value=""></asp:ListItem>
<asp:ListItem Text="Ref No" Value="TRANREFNUM"></asp:ListItem>
<asp:ListItem Text="Invoice No" Value="INVOICE_NO"></asp:ListItem>
<asp:ListItem Text="Auth Code" Value="AUTHCODE"></asp:ListItem>
<asp:ListItem Text="Date" Value="AUTH_TRANS_DATE"></asp:ListItem>
<asp:ListItem Text="CC" Value="MASKEDCC"></asp:ListItem>
			 </asp:DropDownList>
</td>
<td>
<asp:TextBox id="Field_search_txt" runat="server" CssClass="lbfont2"></asp:TextBox>
</td>
<td>
<asp:Button runat="server" ID="field_serch" CssClass="btn" Text="GO" />
</td>
</tr>
</table>
</td>
<td>
<asp:Panel ID="PanelLOV_s" runat="server" visible="False" ScrollBars="Auto" GroupingText="Search Details" Width="350px" Height="150px" CssClass="t_RegionHeader" >
<br>
<asp:DataGrid ID="search_dbg" runat="server" AutoGenerateColumns="False"  CssClass="gridRowCls" AlternatingItemStyle-CssClass="gridAltRowCls"  GridLines="None"> 
                  <HeaderStyle CssClass="detailHdrCls"/>
                  <Columns>
 <asp:TemplateColumn HeaderText="" Visible="True" HeaderStyle-Font-Bold="true">
    	<ItemTemplate>
   	 	<asp:CheckBox  ID="CheckBox0" runat="server" AutoPostBack="True" Checked="true"  OnCheckedChanged="checkbox0_change_s"  CssClass="lbfont3"   />
	</ItemTemplate>
</asp:TemplateColumn>
 <asp:TemplateColumn HeaderText="Field" Visible="false" HeaderStyle-Font-Bold="true">
    <ItemTemplate>
	<asp:Label  ID="S_Field" runat="server" Text='<%#Container.DataItem("s_field")%>' CssClass="lbfont"  width="150px"></asp:Label>
   </ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Field" Visible="True" HeaderStyle-Font-Bold="true">
    <ItemTemplate>
	<asp:Label  ID="S_Field_title" runat="server" Text='<%#Container.DataItem("s_field_title")%>' CssClass="lbfont"  width="150px"></asp:Label>
   </ItemTemplate>
</asp:TemplateColumn>
 <asp:TemplateColumn HeaderText="" Visible="True" HeaderStyle-Font-Bold="true">
    <ItemTemplate>
	<asp:Label  ID="S_con" runat="server" Text='=' CssClass="lbfont"  width="7px"></asp:Label>
   </ItemTemplate>
</asp:TemplateColumn>
 <asp:TemplateColumn HeaderText="Value" Visible="True" HeaderStyle-Font-Bold="true">
    <ItemTemplate>
	<asp:Label  ID="S_Field_value" runat="server" Text='<%#Container.DataItem("s_field_value")%>' CssClass="lbfont3"  ></asp:Label>
   </ItemTemplate>
</asp:TemplateColumn>
 </Columns>
</asp:DataGrid>
</asp:Panel>
</td>
</tr>
</Table>
 <asp:Panel ID="Panel513" runat="server" ScrollBars="Auto" GroupingText="Select Ref No"  CssClass="t_RegionHeader"  >                       
    <asp:Button CssClass="btn" runat="server" ID="Clear"  Text="Clear Filter" ToolTip="Clear Filter"   />                          
   <br>                      
                          <asp:DataGrid ID="db_cust" runat="server" 
                                        	AutoGenerateColumns="False"  GridLines="Vertical" CssClass="gridRowCls" AlternatingItemStyle-CssClass="gridAltRowCls" 
				                  AllowPaging="True" PageSize="50">
                  <HeaderStyle CssClass="detailHdrCls"/>
				                <Columns>
<asp:BoundColumn DataField="TRANREFNUM" HeaderText="TRANREFNUM" Visible="False" />
						        <asp:TemplateColumn HeaderText="Ref No">
						           <HeaderTemplate>
							        <b><asp:Label ID="TRANREFNUM_lb" runat="server"  CssClass="lbfont6" Text="Ref No"   /></b>
                                             		</HeaderTemplate>
						                <ItemTemplate>
							                <asp:Label id="L_TRANREFNUM" runat="server" Text='<%# wrap(Container.DataItem("TRANREFNUM").Tostring(),50) %>' ></asp:Label>
						                </ItemTemplate>
					                    </asp:TemplateColumn>
					                    
					                    
						        <asp:TemplateColumn HeaderText="Invoice No">
						           <HeaderTemplate>
							        <b><asp:Label ID="INVOICE_NO_lb" runat="server"  CssClass="lbfont6" Text="Invoice No"   /></b>
                                             		</HeaderTemplate>
						                <ItemTemplate>
							                <asp:Label id="L_INVOICE_NO" runat="server" Text='<%# wrap(Container.DataItem("INVOICE_NO").Tostring(),50) %>' ></asp:Label>
						                </ItemTemplate>
					                    </asp:TemplateColumn>
					                    
					                    
						        <asp:TemplateColumn HeaderText="Auth Code">
						           <HeaderTemplate>
							        <b><asp:Label ID="AUTHCODE_lb" runat="server"  CssClass="lbfont6" Text="Auth Code"   /></b>
                                             		</HeaderTemplate>
						                <ItemTemplate>
							                <asp:Label id="L_AUTHCODE" runat="server" Text='<%# wrap(Container.DataItem("AUTHCODE").Tostring(),50) %>' ></asp:Label>
						                </ItemTemplate>
					                    </asp:TemplateColumn>
					                    
					                    
						        <asp:TemplateColumn HeaderText="Date">
						           <HeaderTemplate>
							        <b><asp:Label ID="AUTH_TRANS_DATE_lb" runat="server"  CssClass="lbfont6" Text="Date"   /></b>
                                             		</HeaderTemplate>
						                <ItemTemplate>
							                <asp:Label id="L_AUTH_TRANS_DATE" runat="server" Text='<%# wrap(Container.DataItem("AUTH_TRANS_DATE").Tostring(),50) %>' ></asp:Label>
						                </ItemTemplate>
					                    </asp:TemplateColumn>
					                    
					                    
						        <asp:TemplateColumn HeaderText="CC">
						           <HeaderTemplate>
							        <b><asp:Label ID="MASKEDCC_lb" runat="server"  CssClass="lbfont6" Text="CC"   /></b>
                                             		</HeaderTemplate>
						                <ItemTemplate>
							                <asp:Label id="L_MASKEDCC" runat="server" Text='<%# wrap(Container.DataItem("MASKEDCC").Tostring(),50) %>' ></asp:Label>
						                </ItemTemplate>
					                    </asp:TemplateColumn>
					                    
					                    
					                   
				                    </Columns>
				 <PagerStyle HorizontalAlign="Center" ForeColor="Black" BackColor="#999999" Mode="NumericPages" ></PagerStyle>
                          </asp:DataGrid>
 </asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</div>
</form>
</body>
</html>
