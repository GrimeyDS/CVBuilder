using CVBuilder.Pdf.Models;
using CVBuilder.Shared.Constants;
using CVBuilder.Shared.Models;
using CVBuilder.Shared.Queries;
using MediatR;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;

namespace CVBuilder.Pdf;

public class PDFService : IPDFService
{
    private readonly IMediator _mediator;
    private readonly ProfileModel _profile;
    private readonly PDFTemplateModel _template;

    private readonly PdfDocument document;
    private PdfPage page = default!;
    private XGraphics gfx = default!;
    private XTextFormatter tf = default!;

    private double xPoint = 20;
    private double yPoint = 20;
    private readonly double sectionWidth;
    private readonly double margin = 60;
    private readonly double titleHeight = 25;
    private readonly double footerHeight = 100;

    private XColor color1 = default!;
    private XColor color2 = default!;
    private XColor color3 = default!;

    private XBrush brushColor1 = default!;
    private XBrush brushColor2 = default!;
    private XBrush brushColor3 = default!;

    private readonly XFont titleFont = new XFont("Arial", 30, XFontStyleEx.Bold);
    private readonly XFont subTitleFont = new XFont("Arial", 16, XFontStyleEx.Bold);
    private readonly XFont textFont = new XFont("Arial", 12);
    private readonly XFont subTextFont = new XFont("Arial", 10);

    private XImage footerLogo = default!;

    private readonly Dictionary<string, string> icons = new()
    {
        { "phone", "..\\CVBuilder.Pdf\\Constants\\Icons\\phone.png" },
        { "email", "..\\CVBuilder.Pdf\\Constants\\Icons\\email.png" },
        { "location", "..\\CVBuilder.Pdf\\Constants\\Icons\\location.png" },
        { "website", "..\\CVBuilder.Pdf\\Constants\\Icons\\web.png" }
    };

    public PDFService(ProfileModel profile, PDFTemplateModel template, IMediator mediator)
    {
        _mediator = mediator;
        _profile = profile;
        _template = template;

        document = new PdfDocument();
        CreatePage();
        AssignColors();

        sectionWidth = page.Width.Point - (2 * margin);
    }

    private void AssignColors()
    {
        var stringColor1 = _template.PrimaryColor;
        var stringColor2 = _template.SecondColor;
        var stringColor3 = _template.ThirdColor;

        color1 = ConvertToXColor(stringColor1);
        color2 = ConvertToXColor(stringColor2);
        color3 = ConvertToXColor(stringColor3);

        brushColor1 = new XSolidBrush(color1);
        brushColor2 = new XSolidBrush(color2);
        brushColor3 = new XSolidBrush(color3);
    }

    private static XColor ConvertToXColor(string color)
    {
        var components = color.Split(',');

        if (components.Length != 4)
        {
            throw new ArgumentException("Invalid color format. Must be 'A,R,G,B'.");
        }

        byte a = byte.Parse(components[0]);
        byte r = byte.Parse(components[1]);
        byte g = byte.Parse(components[2]);
        byte b = byte.Parse(components[3]);

        return XColor.FromArgb(a, r, g, b);
    }

    public async Task<byte[]> CreatePdf()
    {
        document.Info.Title = $"{PDFConstants.DocumentTitle} - {_profile.LastName} {_profile.FirstName}";
        footerLogo = await GetImage(_template.FooterImageUrl, ContainerNames.pdfpicture);

        await DrawFrontPage();
        CreatePage();

        DrawIntroductionPage();
        DrawFooter();
        CreatePage();

        DrawAboutPage();
        DrawFooter();
        CreatePage();

        await DrawProjects();
        DrawFooter();
        CreatePage();

        DrawBackPage();

        using var stream = new MemoryStream();
        document.Save(stream);
        return stream.ToArray();
    }

    private void CreatePage()
    {
        page = document.AddPage();
        gfx = XGraphics.FromPdfPage(page);
        tf = new XTextFormatter(gfx);
        tf.Alignment = XParagraphAlignment.Right;
        yPoint = margin;
        xPoint = margin;
    }

    private async Task<XImage> GetImage(string pictureName, ContainerNames containerName)
    {
        var query = new GetBlobQuery(pictureName, containerName.ToString());
        var response = await _mediator.Send(query);

        var stream = new MemoryStream();
        stream.Write(response.FileContent, 0, response.FileContent.Length);
        stream.Position = 0;
        return XImage.FromStream(stream);
    }

    private void DrawFooter()
    {
        var pageNumber = document.Pages.Count;
        var middlePage = page.Width / 2;

        // draw number in middle
        gfx.DrawString($"{pageNumber}", new XFont("Arial", 15), brushColor1, new XPoint(middlePage.Point, page.Height.Point - 30), XStringFormats.Center);

        // draw logo in the right corner
        gfx.DrawImage(footerLogo, page.Width.Point - 75, page.Height.Point - 80, 60, 65);
    }

    private async Task DrawFrontPage()
    {
        xPoint = 0;
        yPoint = 0;

        var xMiddle = page.Width / 2;
        var yMiddle = page.Height / 2;

        // Draw Background
        gfx.DrawRectangle(brushColor1, xPoint, yPoint, page.Width.Point - 2 * xPoint, page.Height.Point - 2 * yPoint);
        yPoint += 100;

        // Draw Titles
        gfx.DrawString(PDFConstants.CVTitle, new XFont("Arial", 50, XFontStyleEx.Bold), brushColor2, new XPoint(xMiddle.Point, yPoint), XStringFormats.Center);
        yPoint += 80;
        gfx.DrawString($"{_profile.FirstName} {_profile.LastName}", new XFont("Arial", 30, XFontStyleEx.Bold), brushColor2, new XPoint(xMiddle.Point, yPoint), XStringFormats.Center);
        yPoint += 30;
        gfx.DrawString(_profile.CurrentRole, new XFont("Arial", 20, XFontStyleEx.Regular), brushColor2, new XPoint(xMiddle.Point, yPoint), XStringFormats.Center);
        yPoint += 80;

        // Draw Picture with Frame
        gfx.DrawEllipse(new XPen(color2, 20), brushColor2, xMiddle.Point - 75, yPoint, 150, 150);

        gfx.Save(); // Use Save and restore to reset clipPath.
        XGraphicsPath clipPath = new XGraphicsPath();
        clipPath.AddEllipse(xMiddle.Point - 75, yPoint, 150, 150);
        gfx.IntersectClip(clipPath);
        var profilePicture = await GetImage(_profile.PictureUrl, ContainerNames.profilepicture);
        gfx.DrawImage(profilePicture, xMiddle.Point - 75, yPoint, 150, 150);
        gfx.Restore();
        yPoint += 175;

        // draw Logo
        var logo = await GetImage(_template.LogoUrl, ContainerNames.pdfpicture);
        gfx.DrawImage(logo, xMiddle.Point - 225, yPoint, 450, 125);
        yPoint += 150;

        // draw Subtitle
        gfx.DrawString(_template.SubTitle, new XFont("Arial", 20, XFontStyleEx.Bold), brushColor2, new XPoint(xMiddle.Point, yPoint), XStringFormats.Center);
        yPoint += 75;

        // draw Contact Information
        var companyInfoFont = new XFont("Arial", 12, XFontStyleEx.Bold);

        var maxWidth = gfx.MeasureString(_template.Address, companyInfoFont);
        var xPointRight = page.Width.Point - 30;
        var xPointLeft = xPointRight - maxWidth.Width;

        XGraphicsPath iconbackgroundPath = new XGraphicsPath();

        var xIconPoint = xPointLeft - 25;

        // draw address
        iconbackgroundPath.AddEllipse(xIconPoint, yPoint - 15, 20, 20);
        gfx.DrawPath(new XPen(color2, 1), brushColor2, iconbackgroundPath);
        XImage addressIcon = XImage.FromFile(icons["location"]);
        gfx.DrawImage(addressIcon, xIconPoint + 2.5, yPoint - 12.5, 15, 15);
        gfx.DrawString(_template.Address, companyInfoFont, brushColor2, new XPoint(xPointLeft, yPoint));
        yPoint += 30;

        // draw phone number
        iconbackgroundPath = new XGraphicsPath();
        iconbackgroundPath.AddEllipse(xIconPoint, yPoint - 15, 20, 20);
        gfx.DrawPath(new XPen(color2, 1), brushColor2, iconbackgroundPath);
        XImage phoneIcon = XImage.FromFile(icons["phone"]);
        gfx.DrawImage(phoneIcon, xIconPoint + 2.5, yPoint - 12.5, 15, 15);
        gfx.DrawString(_template.PhoneNumber, companyInfoFont, brushColor2, new XPoint(xPointLeft, yPoint));
        yPoint += 30;

        // draw email
        iconbackgroundPath = new XGraphicsPath();
        iconbackgroundPath.AddEllipse(xIconPoint, yPoint - 15, 20, 20);
        gfx.DrawPath(new XPen(color2, 1), brushColor2, iconbackgroundPath);
        XImage emailIcon = XImage.FromFile(icons["email"]);
        gfx.DrawImage(emailIcon, xIconPoint + 2.5, yPoint - 12.5, 15, 15);
        gfx.DrawString(_template.Email, companyInfoFont, brushColor2, new XPoint(xPointLeft, yPoint));
        yPoint += 30;

        // draw website
        iconbackgroundPath = new XGraphicsPath();
        iconbackgroundPath.AddEllipse(xIconPoint, yPoint - 15, 20, 20);
        gfx.DrawPath(new XPen(color2, 1), brushColor2, iconbackgroundPath);
        XImage websiteIcon = XImage.FromFile(icons["website"]);
        gfx.DrawImage(websiteIcon, xIconPoint + 2.5, yPoint - 12.5, 15, 15);
        gfx.DrawString(_template.Website, companyInfoFont, brushColor2, new XPoint(xPointLeft, yPoint));
    }

    private void DrawIntroductionPage()
    {
        // Title
        gfx.DrawString(PDFConstants.IntroductionTitle, titleFont, brushColor1, new XPoint(xPoint, yPoint));
        yPoint += titleHeight;

        // introduction Description
        double rectHeight = 500;
        XRect rectDescription = new XRect(xPoint, yPoint, sectionWidth - 30, rectHeight);
        tf.Alignment = XParagraphAlignment.Justify;
        tf.DrawString(_profile.Description, textFont, brushColor1, rectDescription);
    }

    private void DrawAboutPage()
    {
        double rectWidth = ((page.Width.Point - (2 * margin)) / 2) - 10;
        double rectHeight = page.Height.Point - margin - footerHeight - 20;

        XRect leftColumn = new XRect(xPoint, yPoint, rectWidth, rectHeight);
        XRect rightColumn = new XRect(xPoint + rectWidth + 20, yPoint, rectWidth, rectHeight);

        // About
        gfx.DrawString(PDFConstants.AboutMeTitle, subTitleFont, brushColor1, new XPoint(leftColumn.X, yPoint));
        yPoint += 20;

        gfx.DrawString($"{PDFConstants.BirthDateLabel} {_profile.BirthDate.ToString("dd/MM/yyyy")}", textFont, brushColor1, new XPoint(leftColumn.X, yPoint));
        yPoint += 20;
        gfx.DrawString($"{PDFConstants.YearsOfExperienceLabel} {_profile.YearsOfExperience}", textFont, brushColor1, new XPoint(leftColumn.X, yPoint));
        yPoint += 40;

        // Tags
        if (_profile.Tags is null)
        {
            gfx.DrawString(PDFConstants.NoTags, subTextFont, brushColor1, new XPoint(leftColumn.X, yPoint));
            yPoint += 20;
            return;
        }
        else
        {
            gfx.DrawString(PDFConstants.TagsTitle, subTitleFont, brushColor1, new XPoint(leftColumn.X, yPoint));
            yPoint += 15;
            var tags = _profile.Tags.Select(t => t.Title).ToList();
            foreach (var tag in tags)
            {
                gfx.DrawEllipse(brushColor2, leftColumn.X + 10, yPoint, 5, 5);
                gfx.DrawString(tag, subTextFont, brushColor1, new XPoint(leftColumn.X + 25, yPoint + 7));
                yPoint += 15;
            }
        }


        yPoint = margin;

        // Education
        if (_profile.Experiences is null)
        {
            gfx.DrawString(PDFConstants.NoEducation, subTextFont, brushColor1, new XPoint(rightColumn.X, yPoint));
            yPoint += 20;
            return;
        }
        else
        {
            gfx.DrawString(PDFConstants.EducationTitle, subTitleFont, brushColor1, new XPoint(rightColumn.X, yPoint));
            yPoint += 15;
            var educations = _profile.Experiences.Where(e => e.Type == "Education").ToList();
            foreach (var education in educations)
            {
                gfx.DrawEllipse(brushColor2, rightColumn.X + 10, yPoint, 5, 5);
                gfx.DrawString(education.Title, textFont, brushColor1, new XPoint(rightColumn.X + 25, yPoint + 7));
                yPoint += 20;
                string endDate = education.EndDate == null ? PDFConstants.Present : education.EndDate.Value.ToString("MMMM yyyy");
                gfx.DrawString($"{education.StartDate.ToString("MMMM yyyy")} - {endDate}", subTextFont, brushColor2, new XPoint(rightColumn.X +25, yPoint));
                yPoint += 15;
                gfx.DrawString(education.Description, subTextFont, brushColor1, new XPoint(rightColumn.X + 25, yPoint));
                yPoint += 15;
            }
        }
        yPoint += 40;

        // Employment
        if (_profile.Experiences is null)
        {
            gfx.DrawString(PDFConstants.NoEmployment, subTextFont, brushColor1, new XPoint(rightColumn.X, yPoint));
            yPoint += 20;
            return;
        }
        else
        {
            gfx.DrawString(PDFConstants.EmploymentTitle, subTitleFont, brushColor1, new XPoint(rightColumn.X, yPoint));
            yPoint += 15;
            var jobs = _profile.Experiences.Where(e => e.Type == "Job").ToList();
            foreach (var job in jobs)
            {
                gfx.DrawEllipse(brushColor2, rightColumn.X + 10, yPoint, 5, 5);
                gfx.DrawString(job.Title, textFont, brushColor1, new XPoint(rightColumn.X + 25, yPoint + 7));
                yPoint += 20;
                string endDate = job.EndDate == null ? PDFConstants.Present : job.EndDate.Value.ToString("MMMM yyyy");
                gfx.DrawString($"{job.StartDate.ToString("MMMM yyyy")} - {endDate}", subTextFont, brushColor2, new XPoint(rightColumn.X + 25, yPoint));
                yPoint += 15;
                gfx.DrawString(job.Description, subTextFont, brushColor1, new XPoint(rightColumn.X + 25, yPoint));
                yPoint += 15;
            }
        }

    }

    private void DrawBackPage()
    {
        xPoint = 0;
        yPoint = 0;

        var xMiddle = page.Width / 2;
        var yMiddle = page.Height / 2;

        // Draw Background
        gfx.DrawRectangle(brushColor1, xPoint, yPoint, page.Width.Point - 2 * xPoint, page.Height.Point - 2 * yPoint);

        // Draw Logo
        gfx.DrawImage(footerLogo, xMiddle.Point - 75, yMiddle.Point - 85 , 150, 175);
    }

    private async Task DrawProjects()
    {
        var amountOfProjects = _profile.Projects.Count;
        for (int i = 0; i < amountOfProjects; i++)
        {
            // for every 3 projects, create a new page
            if (i % 3 == 0 && i != 0)
            {
                DrawFooter();
                CreatePage();
            }

            if (i % 3 != 0)
                gfx.DrawLine(new XPen(color2, 1), (page.Width.Point / 2) - 100, yPoint, (page.Width.Point / 2) + 100, yPoint);
            yPoint += 30;
            await DrawProject(_profile.Projects[i]);
        }

    }

    private async Task DrawProject(ProjectModel project)
    {
        // get current folder path
        var projectImage = await GetImage(project.PictureUrl, ContainerNames.projectpicture);

        // Draw: Customer - Title
        gfx.DrawString($"{project.Customer} - {project.Title}", subTitleFont, brushColor1, new XPoint(xPoint, yPoint));
        yPoint += 15;

        // Draw: Date - Date
        string endDate = project.EndDate == null ? PDFConstants.Present : project.EndDate.Value.ToString("MMMM yyyy");
        gfx.DrawString($"{project.StartDate.ToString("MMMM yyyy")} - {endDate}", textFont, brushColor2, new XPoint(xPoint, yPoint));
        yPoint += 7;

        // Draw Description
        double rectHeight = 80;
        XRect rectDescription = new XRect(xPoint, yPoint, sectionWidth - 30, rectHeight);
        tf.Alignment = XParagraphAlignment.Justify;
        tf.DrawString(project.Description, subTextFont, brushColor1, rectDescription);
        yPoint += rectHeight + 5;

        // Draw Image - Description and tags
        var columnHeight = ((page.Height.Point - margin - footerHeight) / 3) - 140;
        var rightColumnWidth = (page.Width.Point - (2 * margin));

        XRect leftColumn = new XRect(xPoint, yPoint, columnHeight, columnHeight);
        XRect rightColumn = new XRect(xPoint + leftColumn.Width + 20, yPoint, rightColumnWidth, columnHeight);

        gfx.DrawImage(projectImage, leftColumn.X, leftColumn.Y - 7, columnHeight, columnHeight);

        // draw tagsTitle
        gfx.DrawString(PDFConstants.TagsTitle, textFont, brushColor2, new XPoint(rightColumn.X, yPoint));
        yPoint += 15;

        // draw tags

        if (project.Tags is null)
        {
            gfx.DrawString(PDFConstants.NoTags, subTextFont, brushColor1, new XPoint(rightColumn.X, yPoint));
            yPoint += columnHeight + 10;
            return;
        }
        else
        {
            var tags = project.Tags.Select(t => t.Title).ToList();
            var tagsString = string.Join(", ", tags);
            DrawTextWithWrapping(tagsString, subTextFont, new XRect(rightColumn.X, yPoint, rightColumn.Width, rightColumn.Height));
        }
        yPoint += columnHeight;
    }

    private void DrawTextWithWrapping(string text, XFont font, XRect rect)
    {
        double lineHeight = font.GetHeight();
        double availableWidth = rect.Width - (margin * 2);
        double currentY = rect.Y;

        string currentLine = string.Empty;
        string[] words = text.Split(new[] { ", " }, StringSplitOptions.None);

        foreach (string word in words)
        {
            string testLine = string.IsNullOrEmpty(currentLine) ? word : $"{currentLine}, {word}";
            double testLineWidth = gfx.MeasureString(testLine, font).Width;

            if (testLineWidth <= availableWidth)
            {
                currentLine = testLine;
            }
            else
            {
                gfx.DrawString(currentLine, font, XBrushes.Black, new XPoint(rect.X, currentY));
                currentY += lineHeight;
                currentLine = word;
            }
        }
        if (!string.IsNullOrEmpty(currentLine))
        {
            gfx.DrawString(currentLine, font, XBrushes.Black, new XPoint(rect.X, currentY));
        }
    }
}
