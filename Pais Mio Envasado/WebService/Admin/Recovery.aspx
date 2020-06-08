<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Recovery.aspx.cs" Inherits="WebService.Admin.Recovery" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>País Mío</title>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/css/bootstrap.min.css">
    <link href="CSS/Recovery.css" rel="stylesheet" />
</head>
<body>

    <div class="container">
        <div class="row">
            <div class="col-lg-10 col-xl-9 mx-auto">
                <div class="card card-signin flex-row my-5">
                    <div class="card-img-left d-none d-md-flex">
             <!-- Background image for card set in CSS! -->
          </div>
                    <div class="card-body">
                        <form id="form1" runat="server" class="form-label-group">
                            <div class="form-label-group">
                                    <h3>Usted ha solicitado un nueva contreña</h3>
                            </div>
                             <div class="form-label-group">
                                    <p>Por favor revise nuevamente su correo</p>
                            </div>

                          <%-- <div class="form-label-group">
                                <p><asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ErrorMessage="Campo requerido" Display="Dynamic" ControlToValidate="txtEmail" ForeColor="#CC0000"></asp:RequiredFieldValidator></p>
                            </div>

                            <div class="form-label-group">
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Password" ></asp:TextBox>    
                                <label for="txtPassword">Contraseña</label>
                            </div>

                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Campo Requerido" Display="Dynamic" ControlToValidate="txtPassword" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-laber-group">
                                <asp:Button ID="btnLogin" CssClass="btn btn-lg btn-success btn-block text-uppercase" runat="server" Text="Iniciar sesión" OnClick="btnLogin_Click"/>
                           <</div>--%>

                        </form>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
    <%--<script src="js/bootstrap.min.js"></script>--%>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.0/js/bootstrap.min.js"></script>

</body>
</html>
