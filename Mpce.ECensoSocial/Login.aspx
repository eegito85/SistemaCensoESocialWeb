<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="SisCadEstagio.Login" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<html>
<head runat="server">
    <title>Sistema de Cadastro de Candidatos a Estágio</title>
    <link href="css/estilo2010.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="../js/ajax.js" type="text/javascript"></script>

    <link rel="stylesheet" type="text/css" href="css/estilo2010.css" />

    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" type="text/css" >

    <link href="css/styles.css?1512201601" rel="stylesheet" type="text/css" />

    <style type="text/css">text-transform: initial;
       
        .style1
        {
            font-family: Verdana;
            font-size: x-small;
        }
        .style2
        {
            width: 88px;
        }
        body
        {
              background: #f5f5f5 !important;
        }
        .panel {
            margin-bottom: 20px;
            background-color: #fff !important;
            border: 1px solid transparent !important;
            border-radius: 4px;
            -webkit-box-shadow: 0 1px 1px rgba(0,0,0,.05);
            box-shadow: 0 1px 1px rgba(0,0,0,.05);
            margin: auto;
            padding: 10px !important;
        }
        #Image1
        {
            height: 100px;
            margin-right: 8px;
            margin-bottom: 10px;
        }
        #Image2
        {
            height: 25px;
            margin-bottom: 10px;
        }
        input[type=text], select, input[type=password]
        {
            padding: 6px 12px !important;
        }
        .rodapeLogin {
            position: fixed;
            bottom: 0;
            width: 100%;
            text-align: center;
            padding: 15px;
            background: #ffffff;
            box-shadow: 0px 0px 7px rgba(0,0,0,.4);
        }
        .input-group .form-control {
            position: relative;
            z-index: 2;
            float: left;
            width: 100%;
            margin-left: 0px;
            margin-bottom: 0;
        }
        input
        {
            text-transform: none !important;
            
        }
        </style>
  </head>
<body>
    
    <form id="form1" runat="server">
        <div class="container">  
            <div class="row">
                <div class="col-md-6" style="text-align:center; margin-top: 130px;float:left">
                    <div>
                        <asp:Image ID="Image1" runat="server" ImageUrl="imgs/logo_mpce.png" />
                    </div>
                    <div class="conteudo">
                    </div>
                    <asp:LinkButton ID="btnNovo" runat="server" CssClass="conteudo">Ainda não sou Cadastrado</asp:LinkButton>
                    <span class="style1">|</span>
                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="conteudo" NavigateUrl="~/visoes/ResetSenha.aspx">Esqueci minha Senha</asp:HyperLink>
                </div>
                <div class="col-md-6" style="margin-top: 100px;margin-bottom:100px;">
                    <asp:Login ID="loginCadEstagio" runat="server" BackColor="#EFF3FB" BorderColor="#B5C7DE"
                        BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" DestinationPageUrl="Principal.aspx"
                        Font-Names="Verdana" Font-Size="0.8em" ForeColor="#333333" FailureText="A tentativa de logon não teve êxito. Tente novamente."
                        LoginButtonText="Fazer Logon" PasswordLabelText="Senha:" PasswordRequiredErrorMessage="Senha requerida."
                        RememberMeText="Lembrar na próxima vez" TitleText="Fazer Logon" UserNameLabelText="Usuário:"
                        UserNameRequiredErrorMessage="Nome do Usuário Requirido."
                        EnableTheming="True" RememberMeSet="True" TextLayout="TextOnTop" CssClass="panel">
                        <TextBoxStyle Font-Size="0.8em" />

                        <LoginButtonStyle BackColor="White"
                            Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
                        <LayoutTemplate>
                                
                                    <div align="center"
                                    style="color: #333; ;background-color: #FFFFFF; font-weight: bold; border-bottom: 1px solid #eee; padding: 10px;">
                                    Login
                                </div>
                                <div style="padding: 20px;">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">CPF</asp:Label>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                                                        ControlToValidate="UserName" 
                                                                        ErrorMessage="Nome do Usuário Requirido." 
                                                                        ToolTip="Nome do Usuário Requirido." 
                                                                        ValidationGroup="loginCadEstagio">*</asp:RequiredFieldValidator>
                                            <div class="input-group">
                                                <![if !IE]>
                                                    <span class="input-group-addon" id="basic-addon1">
                                                        <span class="glyphicon glyphicon-user" aria-hidden="true"></span>
                                                    </span>
                                                <![endif]>
                                                <asp:TextBox ID="UserName" runat="server" CssClass="form-control" onkeyUp="mascaracpf(this,this.value)" MaxLength="14"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Senha</asp:Label>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" 
                                                                        runat="server" 
                                                                        ControlToValidate="Password" 
                                                                        ErrorMessage="Senha requirida." 
                                                                        ToolTip="Senha requirida." 
                                                                        ValidationGroup="loginCadEstagio">*</asp:RequiredFieldValidator>
                                            <div class="input-group">
                                                <![if !IE]>
                                                    <span class="input-group-addon" id="Span2">
                                                        <span class="glyphicon glyphicon-lock" aria-hidden="true"></span>
                                                    </span>
                                                <![endif]>
                                                <asp:TextBox ID="Password" runat="server" CssClass="form-control" 
                                                                TextMode="Password"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="max-width: 250px; text-align: center; color:#e53935; margin-top: 10px;margin: auto">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </div>         
                                    <div style="width:100%; text-align: right;">
                                        <asp:Button ID="LoginButton" runat="server" CssClass="btn btn-mpce" style="margin-right:0px;"
                                                CommandName="Login" Text="Acessar" ValidationGroup="loginCadEstagio" />
                                    </div>
                                </div>
                                <!--
                                <div  style="text-align:left;padding-left:15px" class="auto-style1">
                                    <br />
                                    Não consegue efetuar seu cadastro? Ative o modo de exibição de compatibilidade.
                                    <br />
                                    Siga as instruções abaixo: 
                                    <br />
                                    1ª Tecle alt
                                    <br />
                                    2ª Menu Ferramentas
                                    <br />
                                    3ª Configurações do Modo De exibição de compatibilidade
                                    <br />
                                    4ª Click em Adicionar.</td>
                                </div>
                                -->
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </div>
        </div>
    </form>
</body>
</html>