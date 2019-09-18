<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CtrlGrid.ascx.cs" Inherits="Controls_CtrlGrid" %>

<%--KrayGrid - A control by krayknot
Created - 30 July 2019
Use under General public license--%>

<div class="table-responsive">
    <table class="table datatable table-bordered table-striped ">
        <asp:Literal ID="literalHeaders" runat="server"></asp:Literal>
        <asp:Literal ID="literalRows" runat="server"></asp:Literal>
    </table>
</div>