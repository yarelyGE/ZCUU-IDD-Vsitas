using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using WebMatrix.Data;

public partial class Funciones : System.Web.UI.Page
{
    
        public static string returnString(DateTime fecha)
        {

            string x = "";
            string nombreMes = "Enero";

            if (fecha.Month == 2)
            {
                nombreMes = "Febrero";
            }

            if (fecha.Month == 3)
            {
                nombreMes = "Marzo";
            }

            if (fecha.Month == 4)
            {
                nombreMes = "Abril";
            }

            if (fecha.Month == 5)
            {
                nombreMes = "Mayo";
            }

            if (fecha.Month == 6)
            {
                nombreMes = "Junio";
            }

            if (fecha.Month == 7)
            {
                nombreMes = "Julio";
            }

            if (fecha.Month == 8)
            {
                nombreMes = "Agosto";
            }

            if (fecha.Month == 9)
            {
                nombreMes = "Septiembre";
            }

            if (fecha.Month == 10)
            {
                nombreMes = "Octubre";
            }

            if (fecha.Month == 11)
            {
                nombreMes = "Noviembre";
            }

            if (fecha.Month == 12)
            {
                nombreMes = "Diciembre";
            }

            x = fecha.Day + " de " + nombreMes + " del " + fecha.Year;

            return x;
        }

        public static void enviarCorreos(string Correo, string asunto, string cuerpo, string nombreUsuario)
        {
            var bodyHTML = PopulateBody(nombreUsuario, asunto, cuerpo);

            using (MailMessage objMailMessage = new MailMessage())
            {
                System.Net.NetworkCredential objSMTPUserInfo = new System.Net.NetworkCredential();
                SmtpClient objSmtpClient = new SmtpClient();

                objMailMessage.From = new MailAddress("IDDVisitas@zodiacaerospace.com");
                objMailMessage.Subject = asunto;
                objMailMessage.Body = bodyHTML;
                objMailMessage.IsBodyHtml = true;
                objMailMessage.To.Add(new MailAddress(Correo));


                objSmtpClient.EnableSsl = false;
                objSmtpClient = new SmtpClient("11.26.16.2");
                objSMTPUserInfo = new System.Net.NetworkCredential("administrator.chihuahua@zodiacaerospace.com", "Password", "Domain");
                objSmtpClient.Credentials = objSMTPUserInfo;
                objSmtpClient.UseDefaultCredentials = false;
                objSmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                objSmtpClient.Send(objMailMessage);
            }
        }

        private static string PopulateBody(string nombreUsuario, string asunto, string description)
        {
            string body = string.Empty;

            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/emailTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", nombreUsuario);
            body = body.Replace("{Title}", asunto);
            body = body.Replace("{Description}", description);
            return body;

        }

    public static void PDF(string VisitaId)
    {
        var Bpdf = Database.Open("IDDVisitas");
        var DataPDF = Bpdf.QuerySingle(@"SELECT * FROM [Visitantes] WHERE IdVisita = @0", VisitaId);
        Bpdf.Close();

        // Creamos el documento con el tamaño de página tradicional
        Document doc = new Document(iTextSharp.text.PageSize.LETTER, 11, 11, 5, 5);
        // Indicamos donde vamos a guardar el documento
        //Path.Combine(Server.MapPath)
        FileStream fs2 = new FileStream(Path.Combine(HttpContext.Current.Server.MapPath(@"~/PDF/IDDVisitas.pdf")), FileMode.Create, FileAccess.Write, FileShare.None);
        PdfWriter writer = PdfWriter.GetInstance(doc, fs2);
        writer.Open();
        doc.Open();
        var content = writer.DirectContent;
        var page = new Rectangle(doc.PageSize);
        page.Left += doc.LeftMargin;
        page.Right -= doc.RightMargin;
        page.Top -= doc.TopMargin;
        page.Bottom += doc.BottomMargin;
        FontFactory.RegisterDirectories();
        Font fuente = FontFactory.GetFont("Times New Roman");
        Font letraBlanca = FontFactory.GetFont("Times New Roman");
        letraBlanca.SetColor(255, 255, 255);
        Font letraVerde = FontFactory.GetFont("Times New Roman");
        letraVerde.SetColor(0, 153, 51);
        Font letraRoja = FontFactory.GetFont("Times New Roman");
        letraRoja.SetColor(255, 0, 0);
        Font negrita = FontFactory.GetFont("Times New Roman", 11, Font.BOLD);
        Font cursiva = FontFactory.GetFont("Times New Roman", 11, Font.ITALIC);
        Font ambos = FontFactory.GetFont("Times New Roman", Font.BOLD, Font.ITALIC);

        iTextSharp.text.Image IM = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(@"~/img/EncabezadoDePDF.png"));
        IM.BorderWidth = 0;
        IM.Alignment = Element.ALIGN_CENTER;

        PdfPTable tablaEncabezado = new PdfPTable(1);
        tablaEncabezado.WidthPercentage = 95;
        tablaEncabezado.TotalWidth = page.Width - 65;
        tablaEncabezado.LockedWidth = true;
        tablaEncabezado.SetWidths(new float[] { 1f });
        PdfPCell celdaEncabezado = new PdfPCell(IM,true);
        IM.WidthPercentage = 100;
        tablaEncabezado.AddCell(celdaEncabezado);
        doc.Add(tablaEncabezado);

        /*TABLA SECCION1*/
        PdfPTable tableSeccion1 = new PdfPTable(1);
        tableSeccion1.WidthPercentage = 95;
        tableSeccion1.TotalWidth = page.Width - 65;
        tableSeccion1.LockedWidth = true;
        tableSeccion1.SetWidths(new float[] { 1f });
        Phrase titulo1 = new Phrase("SECCION 1: COMPLETADA POR VISITANTE O POR ANFITRION ZLS ESTORNUDA CON MOCASINO", letraBlanca);
        PdfPCell celdaTitulo = new PdfPCell(new Phrase(titulo1));
        celdaTitulo.BackgroundColor = new BaseColor(0, 85, 128);
        tableSeccion1.AddCell(celdaTitulo);
        doc.Add(tableSeccion1);

        /*PRIMERA TABLA*/
        PdfPTable table1 = new PdfPTable(3);
        table1.WidthPercentage = 95;
        table1.TotalWidth = page.Width - 65;
        table1.LockedWidth = true;
        table1.SetWidths(new float[] { 2f, 1f, 1.25f });
        Phrase Unot1 = new Phrase("Nombre del Anfitrion Empleado de ZLS: ", fuente);
        Unot1.Add(new Phrase(new Chunk(DataPDF["nombreEmple"] + " " + DataPDF["apellido1"] + " " + DataPDF["apellido2"] + " " + "(" + DataPDF["No.Empleado_Requi"] + ")")));
        Phrase Unot2 = new Phrase("Num. de telefono: ", fuente);
        Unot2.Add(new Phrase(new Chunk(DataPDF["No.Telefono"])));
        Phrase Unot3 = new Phrase("Fecha: ", fuente);
        Unot3.Add(new Phrase(new Chunk(returnString(DataPDF["Fecha_requi"]))));
        PdfPCell celdaT1 = new PdfPCell(Unot1);
        PdfPCell celdaT2 = new PdfPCell(Unot2);
        PdfPCell celdaT3 = new PdfPCell(Unot3);
        table1.AddCell(celdaT1);
        table1.AddCell(celdaT2);
        table1.AddCell(celdaT3);
        doc.Add(table1);

        /*SEGUNDA TABLA*/
        PdfPTable table2 = new PdfPTable(1);
        table2.WidthPercentage = 95;
        table2.TotalWidth = page.Width - 65;
        table2.LockedWidth = true;
        table2.SetWidths(new float[] { 1f });
        Phrase Dost2 = new Phrase("Nombre completo del visitante: ", fuente);
        Dost2.Add(new Phrase(new Chunk(DataPDF["Nombre_Visitante"])));
        PdfPCell celdaUnoT1 = new PdfPCell(new Phrase(Dost2));
        table2.AddCell(celdaUnoT1);
        doc.Add(table2);


        /*TERCERA TABLA*/
        PdfPTable table3 = new PdfPTable(2);
        table3.WidthPercentage = 95;
        table3.TotalWidth = page.Width - 65;
        table3.LockedWidth = true;
        table3.SetWidths(new float[] { 1f, 1f });
        Phrase tresT1 = new Phrase("Estatus de ciudadania o pais de residencia permanente: ", fuente);
        tresT1.Add(new Phrase(new Chunk(DataPDF["Estatus_ciudadania"])));
        Phrase tresT2 = new Phrase("Identificacion verificada: ", fuente);
        if (DataPDF["Identificacion_verificada"])
        {
            tresT2.Add(new Phrase(new Chunk("Identificacion verificada")));
        }
        else { 
            tresT2.Add(new Phrase(new Chunk("Identificacion NO verificada")));
        }
        PdfPCell celdaTresT1 = new PdfPCell(new Phrase(tresT1));
        PdfPCell celdaTresT2 = new PdfPCell(new Phrase(tresT2));
        table3.AddCell(celdaTresT1);
        table3.AddCell(celdaTresT2);
        doc.Add(table3);

        /*CUARTA TABLA*/
        PdfPTable table4 = new PdfPTable(2);
        table4.WidthPercentage = 95;
        table4.TotalWidth = page.Width - 65;
        table4.LockedWidth = true;
        table4.SetWidths(new float[] { 1f, 1f });
        Phrase cuatroT1 = new Phrase("Nombre de la compania: ", fuente);
        cuatroT1.Add(new Phrase(new Chunk(DataPDF["Nombre_compania"])));
        Phrase cuatroT2 = new Phrase("Ciudad, estado (compania o residencia): ", fuente);
        cuatroT2.Add(new Phrase(new Chunk(DataPDF["Ciudad"])));
        PdfPCell celdaCuatroT1 = new PdfPCell(new Phrase(cuatroT1));
        PdfPCell celdaCuatroT2 = new PdfPCell(new Phrase(cuatroT2));
        table4.AddCell(celdaCuatroT1);
        table4.AddCell(celdaCuatroT2);
        doc.Add(table4);

        /*QUINTA TABLA*/
        PdfPTable table5 = new PdfPTable(3);
        table5.WidthPercentage = 95;
        table5.TotalWidth = page.Width - 65;
        table5.LockedWidth = true;
        table5.SetWidths(new float[] { 1.25f, 1f, 1f });
        Phrase cincoT1 = new Phrase("Fecha(s) de la visita: ", fuente);
        cincoT1.Add(new Phrase(new Chunk(returnString(DataPDF["Fecha_inicio_Visita"]))));
        Phrase cincoT2 = new Phrase("Hasta: ", fuente);
        if (DataPDF["Fecha_final_Visita"] == null)
        {
            cincoT2.Add(new Phrase(new Chunk("No aplica una fecha final")));
        }
        else
        {
            cincoT2.Add(new Phrase(new Chunk(returnString(DataPDF["Fecha_final_Visita"]))));
        }
        Phrase cincoT3 = new Phrase();
        if (!DataPDF["Abierto"])
        {
            cincoT3.Add(new Phrase(new Chunk("Terminado")));
        }
        else
        {
            cincoT3.Add(new Phrase(new Chunk("Abierto")));
        }
        PdfPCell celdaCincoT1 = new PdfPCell(new Phrase(cincoT1));
        PdfPCell celdaCincoT2 = new PdfPCell(new Phrase(cincoT2));
        PdfPCell celdaCincoT3 = new PdfPCell(new Phrase(cincoT3));
        table5.AddCell(celdaCincoT1);
        table5.AddCell(celdaCincoT2);
        table5.AddCell(celdaCincoT3);
        doc.Add(table5);

        /*SEXTA TABLA*/
        PdfPTable table6 = new PdfPTable(1);
        table6.WidthPercentage = 95;
        table6.TotalWidth = page.Width - 65;
        table6.LockedWidth = true;
        table6.SetWidths(new float[] { 1f });
        Phrase seisT1 = new Phrase("Explicar proposito de la visita: ", fuente);
        seisT1.Add(new Phrase(new Chunk(DataPDF["Proposito_Visita"])));
        PdfPCell celdaSeisT1 = new PdfPCell(new Phrase(seisT1));
        table6.AddCell(celdaSeisT1);
        doc.Add(table6);

        /*SEPTIMA TABLA*/
        PdfPTable table7 = new PdfPTable(1);
        table7.WidthPercentage = 95;
        table7.TotalWidth = page.Width - 65;
        table7.LockedWidth = true;
        table7.SetWidths(new float[] { 1f });
        Phrase sieteT1 = new Phrase("Empleados de ZLS a ser visitados: ", fuente);
        sieteT1.Add(new Phrase(new Chunk(DataPDF["Empleados_Visitados"])));
        PdfPCell celdaSieteT1 = new PdfPCell(new Phrase(sieteT1));
        table7.AddCell(celdaSieteT1);
        doc.Add(table7);

        /*OCTAVA TABLA*/
        PdfPTable table8 = new PdfPTable(1);
        table8.WidthPercentage = 95;
        table8.TotalWidth = page.Width - 65;
        table8.LockedWidth = true;
        table8.SetWidths(new float[] { 1f });
        Phrase ochoT1 = new Phrase("Areas de IDD por visitar: ", fuente);
        Phrase Admin = new Phrase("-Administrativo", fuente);
        Phrase Inge = new Phrase("-Ingenieria", fuente);
        Phrase Fabri = new Phrase("-Fabrica", fuente);
        Phrase Otross = new Phrase("-" + DataPDF["Otro"], fuente);
        PdfPCell celdaOchoT1 = new PdfPCell();
        celdaOchoT1.AddElement(new Phrase(ochoT1));
        if (DataPDF["Administrativo"])
        {
            celdaOchoT1.AddElement(new Phrase(Admin));
        }
        if (DataPDF["Ingenieria"])
        {
            celdaOchoT1.AddElement(new Phrase(Inge));
        }
        if (DataPDF["Fabrica"])
        {
            celdaOchoT1.AddElement(new Phrase(Fabri));
        }
        if (DataPDF["Otro"] != "")
        {
            celdaOchoT1.AddElement(new Phrase(Otross));
        }
        table8.AddCell(celdaOchoT1);
        doc.Add(table8);

        /*NOVENA TABLA*/
        PdfPTable table9 = new PdfPTable(1);
        table9.WidthPercentage = 95;
        table9.TotalWidth = page.Width - 65;
        table9.LockedWidth = true;
        table9.SetWidths(new float[] { 1f });
        Phrase nueveT1 = new Phrase("Por favor lea y complete la sección de abajo", negrita);
        PdfPCell celdaNueveT1 = new PdfPCell(new Phrase(nueveT1));
        table9.AddCell(celdaNueveT1);
        doc.Add(table9);

        /*DECIMA TABLA*/
        PdfPTable table10 = new PdfPTable(1);
        table10.WidthPercentage = 95;
        table10.TotalWidth = page.Width - 65;
        table10.LockedWidth = true;
        table10.SetWidths(new float[] { 1f });
        Phrase diezT1 = new Phrase("El/los anfitrión(es) de ZLS entienden que él/ella es responsable de salvaguardar y gestionar adecuadamente cualquier divulgación de" +
            " información técnica según las Políticas y Procedimientos de Control de Exportaciones de ZLS. ", fuente);
        diezT1.Add(new Phrase(new Chunk("Favor de llenar la sección de abajo.", cursiva)));
        PdfPCell celdaDiezT1 = new PdfPCell(new Phrase(diezT1));
        table10.AddCell(celdaDiezT1);
        doc.Add(table10);

        /*ONCEAVA TABLA*/
        PdfPTable table11 = new PdfPTable(1);
        table11.WidthPercentage = 95;
        table11.TotalWidth = page.Width - 65;
        table11.LockedWidth = true;
        table11.SetWidths(new float[] { 1f });
        Phrase onceT1 = new Phrase("¿Exportara la información  técnica controlada será analizada y/o divulgada? ", fuente);
        if (DataPDF["Exportara_informacion"])
        {
            onceT1.Add(new Phrase(new Chunk("Si")));  
        }
        else
        {
            onceT1.Add(new Phrase(new Chunk("No")));
        }
        PdfPCell celdaOnceT1 = new PdfPCell(new Phrase(onceT1));
        table11.AddCell(celdaOnceT1);
        doc.Add(table11);
       
        /*DOCEAVA TABLA*/
        PdfPTable table12 = new PdfPTable(1);
        table12.WidthPercentage = 95;
        table12.TotalWidth = page.Width - 65;
        table12.LockedWidth = true;
        table12.SetWidths(new float[] { 1f });
        Phrase doceT1 = new Phrase("En caso de contestar si, favor de revisar con Control de Exportaciones de ZLS para saber si tiene una licencia de exportación que cubra el propósito específico de esta visita", fuente);
        PdfPCell celdDoceT1 = new PdfPCell(new Phrase(doceT1));
        table12.AddCell(celdDoceT1);
        doc.Add(table12);

        /*TRECEAVA TABLA*/
        PdfPTable table13 = new PdfPTable(2);
        table13.WidthPercentage = 95;
        table13.TotalWidth = page.Width - 65;
        table13.LockedWidth = true;
        table13.SetWidths(new float[] { 1.50f, 1f });
        Phrase treceT1 = new Phrase("En caso de ser Si, proveer  Num de licencia: ", fuente);
        if (DataPDF["No.Licencia_control_exportaciones"] != null)
        {
            treceT1.Add(new Phrase(DataPDF["No.Licencia_control_exportaciones"]));
        }
        else
        {
            treceT1.Add(new Phrase("No aplica"));
        }
        Phrase treceT2 = new Phrase("Fecha de expiracion: ", fuente);
        if (DataPDF["Fecha_expiracion_licencia"] == null)
        {
            treceT2.Add(new Phrase(new Chunk("No aplica")));
        }
        else
        {
            treceT2.Add(new Phrase(new Chunk(returnString(DataPDF["Fecha_expiracion_licencia"]))));
        }
        PdfPCell celdaTreceT1 = new PdfPCell(new Phrase(treceT1));
        PdfPCell celdaTreceT2 = new PdfPCell(new Phrase(treceT2));
        table13.AddCell(celdaTreceT1);
        table13.AddCell(celdaTreceT2);
        doc.Add(table13);

        /*TABLA SECCION2*/
        PdfPTable tableSeccion2 = new PdfPTable(1);
        tableSeccion2.WidthPercentage = 95;
        tableSeccion2.TotalWidth = page.Width - 65;
        tableSeccion2.LockedWidth = true;
        tableSeccion2.SetWidths(new float[] { 1f });
        Phrase titulo_1 = new Phrase("SECCION 2: COMPLETADA POR VISITANTE O POR ANFITRION ZLS", letraBlanca);
        PdfPCell celda_Titulo = new PdfPCell(new Phrase(titulo_1));
        celda_Titulo.BackgroundColor = new BaseColor(0, 85, 128);
        tableSeccion2.AddCell(celda_Titulo);
        doc.Add(tableSeccion2);

        /*TABLA UNO*/
        PdfPTable table_1 = new PdfPTable(2);
        table_1.WidthPercentage = 95;
        table_1.TotalWidth = page.Width - 65;
        table_1.LockedWidth = true;
        table_1.SetWidths(new float[] { 1f, 1f });
        Phrase UnoT_1 = new Phrase("Nombre del revisor: ", fuente);
        UnoT_1.Add(new Phrase(new Chunk(DataPDF["NombreAprobador"] + " " + DataPDF["Apellido1Apro"] + " " + DataPDF["Apellido2Apro"] + " " + "(" + DataPDF["No.Empleado_Aprobador"] + ")")));
        Phrase UnoT_2 = new Phrase("Fecha de investigacion: ", fuente);
        UnoT_2.Add(new Phrase(new Chunk(returnString(DataPDF["Fecha_investigacion"]))));
        PdfPCell celda_UnoT1 = new PdfPCell(new Phrase(UnoT_1));
        PdfPCell celda_UnoT2 = new PdfPCell(new Phrase(UnoT_2));
        table_1.AddCell(celda_UnoT1);
        table_1.AddCell(celda_UnoT2);
        doc.Add(table_1);

        if (DataPDF["Aprobado"])
        {
            /*TABLE DOS*/
            PdfPTable table_2 = new PdfPTable(1);
            table_2.WidthPercentage = 95;
            table_2.TotalWidth = page.Width - 65;
            table_2.LockedWidth = true;
            table_2.SetWidths(new float[] { 1f });
            Phrase DosT_1 = new Phrase("PASA El Anfitrión de ZLS provee el tipo de credencial apropiada. Las personas extranjeras (incluidos los empleados de Zodiac) no están autorizadas a ir a zonas de exportación controladas. Notifique a los supervisores antes de cualquier recorrido de la compañía para permitir el suficiente tiempo para retirar/cubrir los artículos controlados.", letraVerde);
            PdfPCell celda_DosT1 = new PdfPCell(new Phrase(DosT_1));
            table_2.AddCell(celda_DosT1);
            doc.Add(table_2);

            /*TABLA TRES*/
            PdfPTable table_3 = new PdfPTable(1);
            table_3.WidthPercentage = 95;
            table_3.TotalWidth = page.Width - 65;
            table_3.LockedWidth = true;
            table_3.SetWidths(new float[] { 1f });
            Phrase TresT_1 = new Phrase("Credencial autorizada: ", fuente);
            Phrase AR = new Phrase("- “V” Acompañante Requerido", fuente);
            Phrase VC = new Phrase("- “A” Vendedor/Contratista Aprobado (Sin Acompañante)", fuente);
            Phrase Ex = new Phrase("- “FN” Extranjero, Acompañante Requerido ", fuente);
            Phrase Otros = new Phrase("-" + DataPDF["Otra_credencial"], fuente);
            PdfPCell celda_TresT1 = new PdfPCell();
            
            celda_TresT1.AddElement(new Phrase(TresT_1));
            if (DataPDF["Acompanante_requerido"])
            {
                celda_TresT1.AddElement(new Phrase(AR));
            }
            if (DataPDF["Vendedor_contratista"])
            {
                celda_TresT1.AddElement(new Phrase(VC));
            }
            if (DataPDF["Extranjero"])
            {
                celda_TresT1.AddElement(new Phrase(Ex));
            }
            if (DataPDF["Otra_credencial"] != "")
            {
                celda_TresT1.AddElement(new Phrase(Otros));
            }
            
            table_3.AddCell(celda_TresT1);
            doc.Add(table_3);

        }
        else
        {
            /*TABLA CUATRO*/
            PdfPTable table_4 = new PdfPTable(1);
            table_4.WidthPercentage = 95;
            table_4.TotalWidth = page.Width - 65;
            table_4.LockedWidth = true;
            table_4.SetWidths(new float[] { 1f });
            Phrase CuatroT_1 = new Phrase("NO PASA NO SE LE PERMITE al Visitante ir más allá del vestíbulo de la planta.", letraRoja);
            PdfPCell celda_CuatroT1 = new PdfPCell(new Phrase(CuatroT_1));
            table_4.AddCell(celda_CuatroT1);
            doc.Add(table_4);

        }

        PdfPTable pie = new PdfPTable(3);
        pie.WidthPercentage = 95;
        pie.TotalWidth = page.Width - 65;
        pie.LockedWidth = true;
        pie.SetWidths(new float[] { 1f, 1f, 1f});
        Phrase pieTitulo1 = new Phrase("10/27/2017", cursiva);
        Phrase pieFrase = new Phrase("Formato de identificacion", cursiva);
        Phrase pieFrase2 = new Phrase("Pagina 1", cursiva);
        PdfPCell celdaPie = new PdfPCell(new Phrase(pieTitulo1));
        celdaPie.BorderWidth = 0;
        PdfPCell celdaPie2 = new PdfPCell(new Phrase(pieFrase));
        celdaPie2.BorderWidth = 0;
        PdfPCell celdaPie3 = new PdfPCell(new Phrase(pieFrase2));
        celdaPie3.BorderWidth = 0;
        pie.AddCell(celdaPie);
        pie.AddCell(celdaPie2);
        pie.AddCell(celdaPie3);
        doc.Add(pie);

        PdfPTable PIE2 = new PdfPTable(3);
        PIE2.WidthPercentage = 95;
        PIE2.TotalWidth = page.Width - 65;
        PIE2.LockedWidth = true;
        PIE2.SetWidths(new float[] { 1f, 1f, 1f });
        Phrase frase11 = new Phrase("REV N", cursiva);
        Phrase frase12 = new Phrase("EC-260", cursiva);
        Phrase frase13 = new Phrase("N:/Formato de control de acceso de visitantesS", cursiva);
        PdfPCell celda11 = new PdfPCell(new Phrase(frase11));
        celda11.BorderWidth = 0;
        PdfPCell celda12 = new PdfPCell(new Phrase(frase12));
        celda12.BorderWidth = 0;
        PdfPCell celda13 = new PdfPCell(new Phrase(frase13));
        celda13.BorderWidth = 0;
        PIE2.AddCell(celda11);
        PIE2.AddCell(celda12);
        PIE2.AddCell(celda13);
        doc.Add(PIE2);
        

        /*Cerrar documento, escritor y filestream*/
        doc.Close();
        writer.Close();
        fs2.Close();

        /*Crear y descargar pdf*/
        HttpContext.Current.Response.TransmitFile(Path.Combine(HttpContext.Current.Server.MapPath(@"~/PDF/IDDVisitas.pdf")));
        HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=Visita.pdf");
        HttpContext.Current.Response.ContentType = "application/octet-stream";
        HttpContext.Current.Response.Flush();
        File.Delete(Path.Combine(HttpContext.Current.Server.MapPath(@"~/PDF/IDDVisitas.pdf")));
    }
}