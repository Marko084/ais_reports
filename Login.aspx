<%@ Page Title="" Language="C#" MasterPageFile="~/default.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AISReports.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        .login-failure-section {padding:3px;} 
        .module-section {display:inline-block; vertical-align:top;}
        .module-section img { width:749px; border:0px;}
        .module-section span {display:block;}
        .labornet-section {margin:auto; width:749px; border-left: 1px solid #373737; background-color:#3e3e3e;}
        .labornet-section span {color:#373737;padding: 15px 5px 15px 8px;font-size:18px;}
        .labornet-section a {text-decoration:none;}
        
    </style>
     <!--[if IE]>
         <style type="text/css">
            .module-section img { width:753px; border:0px;}
         </style>
        <![endif]-->
    <div class="module-section">
        <asp:Login ID="Login1" runat="server" OnLoggingIn="Login1_LoggingIn" 
                                              CssClass="loginStyle" 
                                              Orientation="Vertical" 
                                              TextLayout="TextOnTop"
                                              TitleText="<span class='sign-in-text'>Sign In</span><span class='welcome-text'>Welcome!</span>" 
                                              LoginButtonStyle-CssClass="loginButtonStyle" 
                                              PasswordRecoveryText="" 
                                              PasswordRecoveryUrl="ForgotPassword.aspx" 
                                              DisplayRememberMe="False"
                                              LoginButtonText="Sign In"
                                              FailureTextStyle-CssClass="ui-state-error ui-corner-all login-failure-section"
                                              FailureTextStyle-ForeColor="Black"
                                              FailureTextStyle-Font-Bold="false">
        </asp:Login>
        <asp:Label runat="server" ID="lblMessage" Style="font-size:10pt;color:Maroon;font-family:Tahoma;" />
    </div>
    <div class="module-section labornet-section">
        <a href="http://www.labornetapp.com" target="_blank">
            <img alt="LaborNET" src="images/labornet.png" />
            <!--<span>AIS has developed a new app called LaborNet that connects moving companies and drivers to quality laborers and helpers.  Please visit www.labornetapp.com or contact us to learn more.</span>-->
        </a>
    </div>
</asp:Content>
