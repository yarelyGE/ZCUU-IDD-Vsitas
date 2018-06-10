<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="js_home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="visitas">
          <div class="slideout-menu">
	        <h3>Menú <a href="#" class="slideout-menu-toggle">×</a></h3>
	        <ul>
		        <li><a href="solicitud.aspx">Solicitar Medición <i class="fa fa-angle-right"></i></a></li>
		        <li><a href="cambiar.aspx">Asignar Prioridad <i class="fa fa-angle-right"></i></a></li>
		        <li><a href="modificar.aspx">Modificar Petición <i class="fa fa-angle-right"></i></a></li>
                <li><a href="administrador.aspx">Administración <i class="fa fa-angle-right"></i></a></li>
                <li><a href="login.aspx">Salir <i class="fa fa-angle-right"></i></a></li>
	        </ul>
        </div><!--.slideout-menu-->

        <a href="#" class="boton-menu slideout-menu-toggle"><i class="fa fa-bars"></i>Menú</a>

        <h1>Requisitor</h1>
        <div class="contenedor">
            <div class="contenido-visitas">
                
                <h3>FORMATO DE IDENTIFICACION DE VISITAS</h3>

                <!--Cuerpo del formato-->
                <label>Nombre del anfitrion de ZLS:</label>
                <asp:Textbox runat="server" ID="nombreZLS" placeholder="" ></asp:Textbox>

                <label>Num. de telefono:</label>
                <asp:Textbox runat="server" ID="NTelefono" placeholder="" ></asp:Textbox>

                <label>Fecha:</label>
                <asp:Textbox runat="server" ID="FechaRequi" TextMode="Date" ></asp:Textbox>
                
                <label>Nombre completo del visitante:</label>
                <asp:Textbox runat="server" ID="NombreVisitante" placeholder="" ></asp:Textbox>

                <label>Estatus de ciudadania o pais de residencia permanente:</label>
                <asp:Textbox runat="server" ID="Estatus" placeholder="" ></asp:Textbox>

                <label>Identificacion verificada</label>
                <asp:CheckBox runat="server" ID="IdentificacionVerif" placeholder="" ></asp:CheckBox>

                <label>Nombre de la compania (si hubiera):</label>
                <asp:Textbox runat="server" ID="NombreCompania" placeholder="" ></asp:Textbox>

                <label>Ciudad:</label>
                <asp:Textbox runat="server" ID="Ciudad" placeholder="" ></asp:Textbox>

                <label>Estado:</label>
                <asp:Textbox runat="server" ID="Estado" placeholder="" ></asp:Textbox>

                <label>Fecha (s) de la visita:</label>
                <asp:Textbox runat="server" ID="FechaIniciV" TextMode="Date" ></asp:Textbox> 

                <label>Hasta:</label>
                <asp:Textbox runat="server" ID="FechaFinalV" TextMode="Date" ></asp:Textbox> 

                <label>Explicar proposito de la visita:</label>
                <asp:Textbox runat="server" ID="Proposito" placeholder="" ></asp:Textbox>

                <label>Empleado de ZLS a ser visitados:</label>
                <asp:Textbox runat="server" ID="NombreVisitado" placeholder="" ></asp:Textbox>
                 
                <label>Marcar TODAS las areas por visitar en IDD</label>
                <label>Administrativo</label>
                <asp:CheckBox runat="server" ID="Administrativo" placeholder="" ></asp:CheckBox>

                <label>Ingenieria</label>
                <asp:CheckBox runat="server" ID="Ingenieria" placeholder="" ></asp:CheckBox>

                <label>Fabrica (manufactura/area de ensamble/fabricacion)</label>
                <asp:CheckBox runat="server" ID="Fabrica" placeholder="" ></asp:CheckBox>

                <label>Otro:</label>
                <asp:Textbox runat="server" ID="Otro" placeholder="" ></asp:Textbox>

                <label id="nota">Por favor lea y complete la seccion de abajo</label>
                <p>El/los anfitrione(s) de ZLS entienden que el/ella es responsable de salvaguardar y gestionar 
                    adecuadamente cualquier divulgacion de informacion tecnica segun politicas y procedimientos 
                    de control de exportacion de ZLS</p>

                <label>Exportara la informacion tecnica controlada sera analizada y/o divulgada?</label><label>Si</label>
                <asp:CheckBox runat="server" ID="si" placeholder="" AutoPostBack="true" OnCheckedChanged="si_CheckedChanged"></asp:CheckBox> <!--Checkbox de exportara informacion-->
                
                <!--Panel (oculto hasta que se seleccion el checkbox anterior)-->
                <asp:Panel ID="panelSi" runat="server" Visible="false"><p>Si es si, favor de llenar el formato EC-261 y continuar con esta seccion</p>
                <p>En caso de contestar si, favor de revisar con Control de Exportaciones de ZSL para saber si 
                    tiene una licencia de exportacion que cubra el proposito especifico de esta visita </p>
                <label>En caso de ser si, proveer Num. de licencia:</label>
                <asp:TextBox runat="server" ID="NoLicencia" placeholder="" ></asp:TextBox>
                <label>Fecha de expiracion:</label>
                <asp:TextBox runat="server" ID="expiracion" TextMode="Date" ></asp:TextBox><br/></asp:Panel> 

                <asp:Button runat ="server" ID="enviar" OnClick="enviar_Click" Text="Guardar"/>
            </div>
        </div>

    </section>
</asp:Content>

