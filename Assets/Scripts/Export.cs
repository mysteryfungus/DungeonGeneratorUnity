using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using System.IO;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

public class Export : MonoBehaviour
{
    public Canvas ui;
    public Camera cam;

    public void OnClick()
    {
        ui.enabled = false;
        StartCoroutine(CaptureAndSaveAsPDF());
    }

    IEnumerator CaptureAndSaveAsPDF()
    {
        cam.orthographicSize = 26;
        string screenPath = "tilemap.png";
        Debug.Log("Trying to make a screenshot...\n");
        ScreenCapture.CaptureScreenshot(screenPath);
        yield return new WaitForSeconds(1f);

        string path = EditorUtility.SaveFilePanel("Экспорт в PDF", "", "Dungeon", "pdf");
        if (!string.IsNullOrEmpty(path))
        {
            PdfDocument doc = new PdfDocument();
            PdfPage page = doc.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            using (XImage img = XImage.FromFile(screenPath))
            {
                page.Width = img.PixelWidth; page.Height = img.PixelHeight;
                gfx.DrawImage(img, 0, 0, page.Width, page.Height);
                doc.Save(path);
            }
            ui.enabled = true;
        }
        else ui.enabled = true; 
    }
}
