<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCertificadoVacunacion.aspx.cs" Inherits="VetSoft.Presentation.Reportes.ReporteCertificadoVacunacion" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Certificado de vacunacion</title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <style type="text/css">
            .auto-style1 {
                font-weight: normal;
            }

            .auto-style2 {
                text-decoration: underline;
            }

            .auto-style3 {
                width: 207px;
                height: 134px;
            }
            @media print{
                .toPrint{
                    display:block
                }
                .notPrint{
                    display:none;
                }
            }
        </style>
        <div class="container notPrint">
            <%--holas--%>
        </div>
        <div class="container toPrint">
            <div>
                <div class="d-flex justify-content-center">
                    <img alt="Logo" class="auto-style3 " src="../Content/images/sertevet1.jpg" />

                </div>
                <div class="d-flex justify-content-center">
                    <span class="auto-style2"><strong>CERTIFICADO DE VACUNACIÓN</strong>
                    </span>
                </div>
            </div>
            <br />
            <div style="font-weight: 700">
                <div class="row">
                    <div class="col-6">
                        Propietario: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PropName" />
                    </div>
                    <div class="col-6">
                        Cliente ID:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PropID" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        Telefono: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PropTel" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        Direccion:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PropDir" />
                    </div>
                </div>
            </div>
            <div class="d-flex justify-content-center">
                <span class="auto-style2"><strong>DESCRIPCIÓN DEL PACIENTE</strong></span>
            </div>
            <br />
            <div style="font-weight: 700">

                <div class="row">
                    <div class="col-6">
                        Paciente ID:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacID" />
                    </div>
                    <div class="col-6">
                        N° microchip o licencia:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacChip" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-6">
                        Especie: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacEsp" />
                    </div>
                    <div class="col-6">
                        Fecha de ingreso:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacFechaI" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-6">
                        Nombre: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacName" />
                    </div>
                    <div class="col-6">
                        Color/pelaje:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacColor" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-6">
                        Raza: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacRaza" />
                    </div>
                    <div class="col-6">
                        Fecha de Nacimiento:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacNac" />
                    </div>
                </div>

                <div class="row">
                    <div class="col-6">
                        Genero: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacGenero" />
                    </div>
                    <div class="col-6">
                        Peso:
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PacPeso" />
                        kg.
                    </div>
                </div>
            </div>
            <br />
            <div class="d-flex justify-content-center">
                <span class="auto-style2"><strong>RESUMEN DE VACUNACIÓN</strong></span>
            </div>
            <br />
            <div class="container">
                <asp:ListView runat="server" ID="VacunasList">
                    <ItemTemplate>
                        
            <div class="row" style="font-weight: 700">
                <div class="col-4">Fecha: <span class="auto-style1"><%# Eval("Fecha") %></span></div>
                <div class="col-4">Vacuna: <span class="auto-style1"><%# Eval("Vacuna") %></span></div>
                <div class="col-4">Proxima Cita: <span class="auto-style1"><%# Eval("Prox") %></span></div>

                </div>

                    </ItemTemplate>
                </asp:ListView>
            </div>
<br />
            <div class="d-flex justify-content-center">
                <span class="auto-style2"><strong>RESUMEN DE Desparasitacion</strong></span>
            </div>
            <br />
            <div class="container">
                <asp:ListView runat="server" ID="DesparasitacionList">
                    <ItemTemplate>
                        
            <div class="row" style="font-weight: 700">
                <div class="col-4">Fecha: <span class="auto-style1"><%# Eval("Fecha") %></span></div>
                <div class="col-4">Vacuna: <span class="auto-style1"><%# Eval("Vacuna") %></span></div>
                <div class="col-4">Proxima Cita: <span class="auto-style1"><%# Eval("Prox") %></span></div>

                </div>

                    </ItemTemplate>
                </asp:ListView>
            </div>
                <div class="d-flex justify-content-center">
                    <p >
                        Ave. Charles de Gualle #54, Villa Carmen, Santo Domingo Este<br />
                        Tel. (809)245-9620 / (849)861-5252 / (809)923-8571<br />
                        sertevet@hotmail.com<br />
                        http://sertevet.blogspot.com 
                    </p>
                </div>
        </div>
    </form>
</body>
</html>
