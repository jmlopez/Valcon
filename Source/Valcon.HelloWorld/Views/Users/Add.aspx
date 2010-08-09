<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Valcon.HelloWorld.Views.Users.Add" %>
<%@ Import Namespace="FubuCore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>
        Add a new user</h2>
    <div class="ui-widget error-container" style="display: none;">
        <div class="ui-state-error ui-corner-all notice">
            <p>
                <span class="ui-icon ui-icon-alert"></span>
                <span class="message"></span>
            </p>
        </div>
    </div>
    <div class="ui-widget success-container" style="display: none;">
        <div class="ui-state-highlight ui-corner-all notice">
            <p>
                <span class="ui-icon ui-icon-info"></span>
                <span class="message"></span>
            </p>
        </div>
    </div>
    <form id="Add-User" action="<%= Urls.UrlFor(Model) %>" method="post">
    <fieldset>
        <div class="row">
            <%= this.LabelFor(m => m.FirstName) %>
            <%= this.InputFor(m => m.FirstName) %>
        </div>
        <div class="row">
            <%= this.LabelFor(m => m.LastName) %>
            <%= this.InputFor(m => m.LastName) %>
        </div>
        <div class="row">
            <%= this.LabelFor(m => m.EmailAddress) %>
            <%= this.InputFor(m => m.EmailAddress)%>
        </div>
        <div class="row">
            <%= this.LabelFor(m => m.Password) %>
            <%= this.InputFor(m => m.Password)%>
        </div>
        <div class="row">
            <%= this.LabelFor(m => m.ConfirmPassword) %>
            <%= this.InputFor(m => m.ConfirmPassword)%>
        </div>
    </fieldset>
    </form>
    <input type="submit" value="Save" id="Create-User" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript" src="<%= "~/Scripts/jquery/jquery-1.4.2.min.js".ToAbsoluteUrl() %>"></script>
    <script type="text/javascript" src="<%= "~/Scripts/jquery/jquery.form.js".ToAbsoluteUrl() %>"></script>
    <script type="text/javascript" src="<%= "~/Scripts/jquery/jquery.validate.min.js".ToAbsoluteUrl() %>"></script>
    <script type="text/javascript" src="<%= "~/Scripts/jquery/jquery-ui-1.8.2.custom.min.js".ToAbsoluteUrl() %>"></script>
    <script type="text/javascript" src="<%= "~/Scripts/core.js".ToAbsoluteUrl() %>"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#Add-User').validate({
                submitHandler: function (form) {
                    $.valcon.clearErrors();
                    $.valcon.showLoadingDialog();
                    $(form).ajaxSubmit({
                        dataType: 'json',
                        type: 'post',
                        success: function (response) {
                            $.valcon.jsonResponseHandler(response);
                        }
                    });
                    $.valcon.closeLoadingDialog();
                }
            });
            $('#Create-User').click(function () {
                $('#Add-User').submit();
            });
        });
    </script>
</asp:Content>
