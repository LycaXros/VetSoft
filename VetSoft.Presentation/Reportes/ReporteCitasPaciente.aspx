<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteCitasPaciente.aspx.cs" Inherits="VetSoft.Presentation.Reportes.ReporteCitasPaciente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Reporte de Registro de ingreso paciente</title>

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

            @media print {
                .toPrint {
                    display: block
                }

                .notPrint {
                    display: none;
                }
            }
        </style>

        <div class="container notPrint">
            <br />
            <span onclick="MakePrint()" class="btn btn-primary">Imprimir </span>|| 
            <asp:HyperLink Text="Volver a la Lista" runat="server" CssClass="btn btn-primary" ID="GoBackID" />

        </div>
        <br />
        <div class="container toPrint">

            <div>
                <div class="d-flex justify-content-center">
                    <img alt="Logo" class="auto-style3 " src="../Content/images/sertevet1.jpg" />

                </div>
                <div class="d-flex justify-content-center">
                    <span class="auto-style2"><strong>REGISTRO DE INGRESO PACIENTE</strong></span><strong><br />
                        <br />
                    </strong><span class="auto-style2"><strong>DESCRIPCION DEL PACIENTE</strong></span>
                </div>
            </div>

            <br />
            <div style="font-weight: 700">
                <div class="row">
                    <div class="col-6">
                        Propietario: 
                        <asp:Label Text="text" runat="server" CssClass="auto-style1" ID="PropName" />
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
            <div class="d-flex justify-content-center">
                <span class="auto-style2"><strong>HISTORIAL CLINICO</strong></span>
            </div>
            <div style="font-weight: 700">
                Atendido por:<span class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Joao Sosa</span><br />
                <%--Pre diagnóstico:<span class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; N/A&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <strong>Costo:</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;$500</span><br />--%>
                <%--Tratamiento:<span class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Internamiento</span><br />--%>

                <div class="row">
                    <div class="col-4">
                        Fecha
                    </div>
                    <div class="col-8">
                        <asp:Label runat="server" ID="fechaLabel"
                            CssClass="auto-style1" Enabled="false" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        Pre-Diagnóstico
                    </div>
                    <div class="col-8">
                        <asp:TextBox runat="server" ID="preDiagText" TextMode="MultiLine"
                            CssClass="auto-style1 form-control" Enabled="false" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-4">
                        Indicaciones
                    </div>
                    <div class="col-8">
                        <asp:ListBox runat="server" ID="IndicacionesList"></asp:ListBox>
                    </div>
                </div>
            </div>
            <br />
            <div class="d-flex justify-content-center">
                <p>
                    Ave. Charles de Gualle #54, Villa Carmen, Santo Domingo Este<br />
                    Tel. (809)245-9620 / (849)861-5252 / (809)923-8571<br />
                    sertevet@hotmail.com
                </p>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var MakePrint = function () {
            window.print();
        };
    </script>
</body>
</html>
