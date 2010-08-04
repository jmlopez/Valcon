<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserListItem.ascx.cs" Inherits="Valcon.HelloWorld.Views.Users.UserListItem" %>
<tr>
    <td><%= this.DisplayFor(u => u.FirstName) %></td>
    <td><%= this.DisplayFor(u => u.LastName) %></td>
    <td><%= this.DisplayFor(u => u.EmailAddress) %></td>
</tr>