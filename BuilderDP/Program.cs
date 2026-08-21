namespace MyApp
{
    public class Report //Product
    {
        public string? ReportType { get; set; }
        public string? ReportHeader { get; set; }
        public string? ReportFooter { get; set; }
        public string? ReportContent { get; set; }
    }

    public abstract class ReportBuilder //Builder abstract class
    {
        protected Report _reportObject;

        public abstract void SetReportType();

        public abstract void SetReportHeader();

        public abstract void SetReportFooter();

        public abstract void SetReportContent();

        protected ReportBuilder()
        {
            _reportObject = new Report();
        }

        public Report GetReport()
        {
            return _reportObject;
        }
    }

    public class ExcelReportBuilder : ReportBuilder    //Concrete Builder classes
    {
        public ExcelReportBuilder() : base()
        {
        }

        public override void SetReportContent()
        {
            _reportObject.ReportContent = "Excel content";
        }

        public override void SetReportFooter()
        {
            _reportObject.ReportFooter = "Excel Footer";
        }

        public override void SetReportHeader()
        {
            _reportObject.ReportHeader = "Excel header";
        }

        public override void SetReportType()
        {
            _reportObject.ReportType = "Excel report type";
        }
    }

    public class PdfReportBuilder : ReportBuilder    //Concrete Builder classes
    {
        public PdfReportBuilder() : base()
        {
        }

        public override void SetReportContent()
        {
            _reportObject.ReportContent = "Pdf content";
        }

        public override void SetReportFooter()
        {
            _reportObject.ReportFooter = "Pdf Footer";
        }

        public override void SetReportHeader()
        {
            _reportObject.ReportHeader = "Pdf header";
        }

        public override void SetReportType()
        {
            _reportObject.ReportType = "Pdf report type";
        }
    }

    public class Director   //Director
    {
        public ReportBuilder? _reportBuilder;

        public Director(ReportBuilder reportBuilder)
        {
            _reportBuilder = reportBuilder;
        }

        public Report MakeReport()     //Defining steps
        {
            if (_reportBuilder != null)
            {
                _reportBuilder.SetReportType();
                _reportBuilder.SetReportHeader();
                _reportBuilder.SetReportContent();
                _reportBuilder.SetReportFooter();
            }

            return _reportBuilder.GetReport();
        }
    }

    internal class Program  //Client
    {
        private static void Main(string[] args)
        {
            var excelBuilder = new ExcelReportBuilder();
            var director = new Director(excelBuilder);
            var report = director.MakeReport();

            Console.WriteLine("ReportType=" + report.ReportType);
            Console.WriteLine("ReportHeader=" + report.ReportHeader);
            Console.WriteLine("ReportContent=" + report.ReportContent);
            Console.WriteLine("ReportFooter=" + report.ReportFooter);

            Console.WriteLine("\n");

            var pdfBuilder = new PdfReportBuilder();
            director = new Director(pdfBuilder);
            var pdf = director.MakeReport();

            Console.WriteLine("ReportType=" + pdf.ReportType);
            Console.WriteLine("ReportHeader=" + pdf.ReportHeader);
            Console.WriteLine("ReportContent=" + pdf.ReportContent);
            Console.WriteLine("ReportFooter=" + pdf.ReportFooter);
        }
    }
}