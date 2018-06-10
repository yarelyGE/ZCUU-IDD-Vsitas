<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="NuevaVisita.aspx.cs" Inherits="NuevaVisita" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section class="NuevaVisitas">
          <%--<div class="slideout-menu">
	        <h3>Menú <a href="#" class="slideout-menu-toggle">×</a></h3>
	        <ul>
		        <li><a href="NuevaVisita.aspx">Nueva Requisicion<i class="fa fa-angle-right"></i></a></li>
		        <li><a href="Visitas.aspx">Visitas <i class="fa fa-angle-right"></i></a></li>
		        <li id="liUsuarios" runat="server"><a href="Usuarios.aspx">Usuarios <i class="fa fa-angle-right"></i></a></li>
                <!--<li><a href="administrador.aspx">Administración <i class="fa fa-angle-right"></i></a></li>-->
                <li><a href="login.aspx">Salir <i class="fa fa-angle-right"></i></a></li>
	        </ul>
        </div><!--.slideout-menu-->

        

        <a href="#" class="boton-menu slideout-menu-toggle"><i class="fa fa-bars"></i>Menú</a>--%>
        <div class="jumbotron jumbotron-fluid">
            <div class="container">
                <div class="titulo">
                     <h1>Registro de visita</h1>
                    <p class="lead">Ingresa / Agenda una nueva visita.</p>
                </div>
            </div>
        </div>
       
        <div class="container wrap">
            <div class="contenido form-visita">
                
                <h4>Formato de Identificacion de Visitas</h4>
                <center>
                   <button type="button" id="Button1" class="btn btn-info" onclick="mostrarDiv('uno')">Paso 1</button>
                   <button type="button" id="Button2" class="btn btn-info" onclick="mostrarDiv('uno_uno')">Paso 2</button>
                   <button type="button" id="Button3" class="btn btn-info" onclick="mostrarDiv('dos')">Paso 3</button>
                   <button type="button" id="Button4" class="btn btn-info" onclick="mostrarDiv('tres')">Paso 4</button>
                    
                      <label id="Error" runat="server" Visible="false" style="color: red">EXISTEN CAMPOS QUE SON OBLIGATORIOS, FAVOR DE COMPLETAR EL FORMULARIO</label>
                    </center>

                <div id="uno" class="row">
                    <div class="col-sm">
                         <label class="titulosHomeUno">Num. de telefono:</label>
                        <asp:Textbox runat="server" ID="NTelefono" ClientIDMode="Static" placeholder="" CssClass="" MaxLength="20" onchange="validarCampos() "></asp:Textbox>

                        <label class="titulosHomeUno">Nombre completo del visitante:</label>
                        <asp:Textbox runat="server" ID="NombreVisitante" ClientIDMode="Static" placeholder="" CssClass=" " MaxLength="50" onchange="validarCampos() "></asp:Textbox>
                    </div>
                   <div class="col-sm">
                       <label class="titulosHomeUno">Estatus de ciudadania o <br />pais de residencia permanente:</label>
                        <asp:Textbox runat="server" ID="Estatus" ClientIDMode="Static" placeholder="" CssClass=" " MaxLength="50" onchange="validarCampos() "></asp:Textbox>

                        <label class="">Identificacion verificada</label>
                        <asp:CheckBox runat="server" ID="IdentificacionVerif" placeholder="" CssClass="redondo textBoxHomeUno"></asp:CheckBox>
                   </div>
                  </div> 

                 
                <div id="uno_uno" class="row">
                    <div class="col-sm">
                        <label class="titulosHomeUnoUno">Nombre de la compania <br /> (si hubiera):</label>
                        <asp:Textbox runat="server" ID="NombreCompania" ClientIDMode="Static" placeholder="" CssClass="redondo textBoxHomeUnoUno" MaxLength="50" onchange="validarCampos() "></asp:Textbox>

                        <label class="titulosHomeUnoUno">Ciudad:</label>
                        <asp:Textbox runat="server" ID="Ciudad" placeholder="" ClientIDMode="Static" CssClass="redondo textBoxHomeUnoUno" MaxLength="50" onchange="validarCampos() "></asp:Textbox>

                        <label class="titulosHomeUnoUno">Estado:</label>
                        <asp:Textbox runat="server" ID="Estado" placeholder="" ClientIDMode="Static" CssClass="redondo textBoxHomeUnoUno" MaxLength="50" onchange="validarCampos() "></asp:Textbox>
                    </div>

                    <div class="col-sm">
                        
                    <label class="titulosHomeUnoUno">Fecha (s) de la visita:</label>
                    <asp:Textbox runat="server" ID="FechaIniciV" TextMode="Date" ClientIDMode="Static" CssClass="redondo textBoxHomeUnoUno" onchange="validarCampos()"></asp:Textbox> 

                    <label class="titulosHomeUnoUno">Tipo de visita</label>
                    <center>
                    <asp:DropDownList ID="TipoV" runat="server" ClientIDMode="Static" CssClass="DropDownListCSS textBoxHomeUnoUno redondo" onchange="validarCampos()">
                        <asp:ListItem Value=""> </asp:ListItem>
                        <asp:ListItem Value="Anual">Anual</asp:ListItem>
                        <asp:ListItem Value="Permanente">Permanente</asp:ListItem>
                        <asp:ListItem Value="Unica">Unica</asp:ListItem>
                    </asp:DropDownList></center>

                    </div>

                </div>

                <div id="dos" class="row">
                     <div class="col-sm">                       
                        <label>Marcar TODAS las areas por visitar en IDD</label>
                        <label class="lead">Administrativo</label>
                        <asp:CheckBox runat="server" ID="Administrativo" ClientIdMode="Static" placeholder="" CssClass="textBoxHome"  onclick="validarCampos()"></asp:CheckBox>

                        <label class="lead">Ingenieria</label>
                        <asp:CheckBox runat="server" ID="Ingenieria" ClientIdMode="Static" placeholder="" CssClass="textBoxHome" onchange="validarCampos()"></asp:CheckBox>

                        <label class="lead">Fabrica (manufactura/area de ensamble/fabricacion)</label>
                        <asp:CheckBox runat="server" ID="Fabrica" ClientIdMode="Static" placeholder="" CssClass="textBoxHome" onchange="validarCampos()"></asp:CheckBox>

                        <label class="lead">Otro:</label>
                        <asp:Textbox runat="server" ID="Otro" ClientIdMode="Static" placeholder="" CssClass="redondo textBoxHome" MaxLength="100" onchange="validarCampos()"></asp:Textbox>                       
                    </div>

                    <div class="col-sm">
                        <label class="titulosHomeUnoUno">Explicar proposito de la visita:</label>
                        <asp:Textbox runat="server" ID="Proposito" placeholder="" ClientIDMode="Static" CssClass="redondo textBoxHome" onchange="validarCampos() " MaxLength="200"></asp:Textbox>

                        <label class="titulosHome">Empleado de ZLS a ser visitados:    </label>
                        <asp:Textbox runat="server" ID="NombreVisitado" placeholder="" ClientIDMode="Static"  CssClass="redondo textBoxHome" onchange="validarCampos() " MaxLength="200"></asp:Textbox>                        
                    </div>
                </div>
                <div id="tres">

                    <label id="nota">Por favor lea y complete la seccion de abajo</label>
                    <p class="lead">El/los anfitrione(s) de ZLS entienden que el/ella es responsable de salvaguardar y gestionar 
                        adecuadamente cualquier divulgacion de informacion tecnica segun politicas y procedimientos 
                        de control de exportacion de ZLS</p>

                    <label>Exportara la informacion tecnica controlada sera analizada y/o divulgada?</label>
                    <label>Si</label>
                    <asp:CheckBox runat="server" ID="si" ClientIDMode="Static" placeholder="" CssClass="textBoxHome" onchange="ocultarPanelSi()"></asp:CheckBox> <!--AutoPostBack="true" OnCheckedChanged="si_CheckedChanged" Checkbox de exportara informacion-->
                
                    <!--Panel (oculto hasta que se seleccion el checkbox anterior)-->
                    <div id="panelSi"><p class="lead">Si es si, favor de llenar el formato EC-261 y continuar con esta seccion</p>
                    <p class="lead">En caso de contestar si, favor de revisar con Control de Exportaciones de ZSL para saber si 
                        tiene una licencia de exportacion que cubra el proposito especifico de esta visita </p>
                    <label class="titulosHome">En caso de ser si, proveer Num. de licencia:</label>
                    <asp:TextBox runat="server" ID="NoLicencia" placeholder="" CssClass="redondo textBoxHome" MaxLength="20"></asp:TextBox>
                    <label>Fecha de expiracion:</label>
                    <asp:TextBox runat="server" ID="expiracion" TextMode="Date" CssClass="redondo textBoxHome"></asp:TextBox><br/></div> 
                 
                    <asp:Button runat ="server" ID="enviar" CssClass="btn btn-success" OnClick="enviar_Click" Text="Guardar" />

                    <script>
                        function ocultarPanelSi() {
                            var checkbox = document.getElementById('si');
                            var panel = document.getElementById('panelSi');
                            if (checkbox.checked == true) {
                                panel.removeAttribute('style');
                            }
                            else {
                                panel.setAttribute('style', 'display:none');
                            }
                        }
                            ocultarPanelSi();
                    </script>
                </div>
            </div>
        </div>

    </section>

    <script>        

            var btn1 = document.getElementById('Button1');
            var btn2 = document.getElementById('Button2');
            var btn3 = document.getElementById('Button3');
            var btn4 = document.getElementById('Button4');
        
            var uno = document.getElementById('uno');
            var uno_uno = document.getElementById('uno_uno');
            var dos = document.getElementById('dos');
            var tres = document.getElementById('tres');

         function habilitarBoton(boton) {
            boton.setAttribute('class', 'btn btn-info');
            boton.removeAttribute('disabled');
        }
        function desHabilitarBoton(boton) {
            boton.setAttribute('class', 'btn  btn-outline-info');
            boton.setAttribute('disabled','disabled');
        }

        function mostrarDiv(div) {
            uno.setAttribute('style','display:none');
            uno_uno.setAttribute('style','display:none');
                dos.setAttribute('style','display:none');
            tres.setAttribute('style', 'display:none');

            document.getElementById(div).setAttribute('style', 'display:normal');
        }

        function ocultarDivs() {
                uno_uno.setAttribute('style','display:none');
                dos.setAttribute('style','display:none');
                tres.setAttribute('style', 'display:none');

                desHabilitarBoton(btn2);
                desHabilitarBoton(btn3);
                desHabilitarBoton(btn4);
            }
        ocultarDivs();

        function validar1() {
            var numtele = document.getElementById('NTelefono');
            var visit = document.getElementById('NombreVisitante');
            var estat = document.getElementById('Estatus');

            if (numtele.value != "" && visit.value != "" && estat.value != "") {
                habilitarBoton(btn2);
            }
            else {
                desHabilitarBoton(btn4);
                desHabilitarBoton(btn3);
                desHabilitarBoton(btn2);
            }
        }

        function validar2() {
            var compania = document.getElementById('NombreCompania');
            var ciudad = document.getElementById('Ciudad');
            var estado = document.getElementById('Estado');
            var fechaIni = document.getElementById('FechaIniciV');
            var tipov = document.getElementById('TipoV');
            

            if (compania.value != "" && ciudad.value != "" && estado.value != "" && fechaIni.value != "" && tipov.value != "" ) {
                habilitarBoton(btn3);
            }
            else {
                desHabilitarBoton(btn4);
                desHabilitarBoton(btn3);
            }
        }
        function validar3() {
            var propo = document.getElementById('Proposito');
            var visitado = document.getElementById('NombreVisitado');
            var adminis = document.getElementById('Administrativo');
            var ingen = document.getElementById('Ingenieria');
            var fa = document.getElementById('Fabrica');
            var otr = document.getElementById('Otro');
            if (propo.value != "" && visitado.value != "" &&
                (adminis.checked || ingen.checked ||  fa.checked || document.getElementById('Otro').value != "")){
                habilitarBoton(btn4);
            }
            else {
                desHabilitarBoton(btn4);
            }
        }

        function validarCampos() {
            validar3();
            validar2();
            validar1();
        }
        validarCampos();
        </script>
</asp:Content>