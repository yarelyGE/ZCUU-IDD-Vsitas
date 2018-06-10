<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="solicitud.aspx.cs" Inherits="solicitud" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="solicitud">
     <div class="slideout-menu">
	        <h3>Menú <a href="#" class="slideout-menu-toggle">×</a></h3>
	        <ul>
		         <li><a href="home.aspx">Nueva Requisicion<i class="fa fa-angle-right"></i></a></li>
		        <!--<li><a href="cambiar.aspx">Asignar Prioridad <i class="fa fa-angle-right"></i></a></li>
		        <li><a href="modificar.aspx">Modificar Petición <i class="fa fa-angle-right"></i></a></li>
                <li><a href="administrador.aspx">Administración <i class="fa fa-angle-right"></i></a></li>-->
                <li><a href="login.aspx">Salir <i class="fa fa-angle-right"></i></a></li>
	        </ul>
        </div><!--.slideout-menu-->

        <a href="#" class="boton-menu slideout-menu-toggle"><i class="fa fa-bars"></i>Menú</a>

        <h1>Solicitud de medición</h1>
        <div class="contenedor">
            <div class="contenido-solicitud clearfix">

                <div class="info1 clearfix">
                   
                    <label>Requisitor</label>
                    <input type="text" runat="server" id="requisitor" disabled />

                    <label>Departamento</label>
                    <input type="text" runat="server" id="departamento" disabled />

                    <label>No. Parte o Herramental</label>
                    <input type="text" id="n_parte" runat="server" autocomplete="off" required autofocus />
                    <ul class="lista-rev">
                        <li><label>Revisión</label></li>
                        <li><input type="text" id="revision" runat="server" required autocomplete="off" maxlength="3" /></li>
                    </ul>
                    <label>Work Order</label>
                    <input type="text" id="wo" runat="server" autocomplete="off" required placeholder="NA si no aplica" />

                    

                </div><!--.info1-->
                
                <div class="info2 clearfix">
                    <label>Tipo de Petición</label>
                    <select runat="server" id="tipo" required>
                        <option value="">Selecciona una opción</option>
                        <option value="FAI">FAI</option>
                        <option value="Herramienta Clase A">Hmts Clase A (QA)</option>
                        <option value="Herramienta Clase B">Hmts Clase (Mfg)</option>
                        <option value="Shop Aid">Shop Aid</option>
                        <option value="Produccion Regular">Prod. Regular (QA)</option>
                    </select>

                    <label>Metodo Dimensionado</label>
                    <select id="dimensionado" runat="server" required>
                        <option value="">Selecciona una opción</option>
                        <option value="Palpador">Palpador</option>
                        <option value="Metodo 3D">Metodo 3D</option>
                        <option value="Palpador/3D">Palpador / 3D</option>
                    </select>                               
                    

                    <label>Dimensiones Requeridas</label>
                    <select runat="server" id="dimensiones" required>
                        <option value="">Selecciona una opción</option>
                        <option value="Full Layout">Full Layout</option>
                        <option value="Caracteristicas">Caracteristicas</option>
                    </select>

                    <label>Tipo de Reporte</label>
                    <select id="reporte" runat="server" required>
                        <option value="">Selecciona una opción</option>
                        <option value="FARO">FARO</option>
                        <option value="Balloon">Balloon</option>
                        <option value="FARO/Balloon">FARO/Balloon</option>
                    </select>  
                    <ul class="lista-apu">
                        <li>
                            <label>APU</label>
                        </li>
                        <li>
                            <select runat="server" id="apu" required>
                            <option value=""></option>
                            <option value="1">1</option>
                            <option value="2">2</option>
                            </select>
                        </li>
                    </ul>
                </div><!--.info2-->
                <div class="info3 clearfix">

                    <label class="nota">Guardar archivo STP o STEP en la siguiente ruta:<br />
                        \\schi-fs1zsh\Facility Share\FARO ARM DOCS<br />
                        De no encontrarse el archivo, la petición se rechazará.</label>

                    <asp:Button ID="Button1" runat="server" class="button transparente" Text="Enviar" OnClick="Button1_Click" />
                </div>
                
            </div><!--.contenido-solicitud-->
        </div><!--.contenedor-->

    </section>
</asp:Content>

