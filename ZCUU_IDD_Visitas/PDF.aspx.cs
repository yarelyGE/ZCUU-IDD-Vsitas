using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

public partial class PDF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
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

        /*PRIMERA TABLA*/
        PdfPTable tableSeccion1 = new PdfPTable(1);
        tableSeccion1.WidthPercentage = 95;
        tableSeccion1.TotalWidth = page.Width - 65;
        tableSeccion1.LockedWidth = true;
        tableSeccion1.SetWidths(new float[] { 1f });
        Phrase titulo1 = new Phrase("SECCION 1: COMPLETADA POR VISITANTE O POR ANFITRION ZLS",fuente);
        PdfPCell celdaTitulo = new PdfPCell(new Phrase(titulo1));
        tableSeccion1.AddCell(celdaTitulo);
        doc.Add(tableSeccion1);

        /*SEGUNDA TABLA*/
        PdfPTable table1 = new PdfPTable(3);
        table1.WidthPercentage = 95;
        table1.TotalWidth = page.Width -65;
        table1.LockedWidth = true;
        table1.SetWidths(new float[] {2f, 1f, 1.25f});
        Phrase t1 = new Phrase("Nombre del Anfitrion Empleado de ZLS:" , fuente);
        Phrase t2 = new Phrase("Num. de telefono:", fuente);
        Phrase t3 = new Phrase("Fecha:", fuente);
        PdfPCell celdaT1 = new PdfPCell(new Phrase(t1));
        PdfPCell celdaT2 = new PdfPCell(new Phrase(t2));
        PdfPCell celdaT3 = new PdfPCell(new Phrase(t3));
        table1.AddCell(celdaT1);
        table1.AddCell(celdaT2);
        table1.AddCell(celdaT3);
        doc.Add(table1);


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