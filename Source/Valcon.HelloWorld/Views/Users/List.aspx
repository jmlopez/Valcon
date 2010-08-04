<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Valcon.HelloWorld.Views.Users.List" %>
<%@ Import Namespace="Valcon.HelloWorld.Views.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Users</h2>
    <table>
        <thead>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email Address</th>
        </thead>
        <tbody>
            <%= this.RenderPartialForEachOf(m => m.Users).Using<UserListItem>() %>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
