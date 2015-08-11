using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using System.ComponentModel;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.XamarinAndroid;
using XamarinCRMAndroid.Renderers;

[assembly: ExportRenderer(typeof(XamarinCRM.CustomControls.PieChart), typeof(PieChartRenderer))]

namespace XamarinCRMAndroid.Renderers
{
    public class PieChartRenderer : ViewRenderer<XamarinCRM.CustomControls.PieChart, PlotView>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<XamarinCRM.CustomControls.PieChart> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;

            var plotModel1 = new PlotModel();
            plotModel1.Title = string.Empty;
            var pieSeries1 = new PieSeries();
            pieSeries1.InsideLabelPosition = 0.8;
            pieSeries1.StrokeThickness = 2;
            plotModel1.Series.Add(pieSeries1);

            foreach (var item in Element.Items)
            {
                pieSeries1.Slices.Add(new PieSlice
                    {
                        Label = item.Name,
                        Value = item.Value
                    });
            }    

            var plotView = new PlotView(Forms.Context);

            //Add padding to prevent cropping
            plotModel1.Padding = new OxyPlot.OxyThickness(30);

            pieSeries1.FontWeight = FontWeights.Bold;
            //pieSeries1.TextColor = OxyColors.White;
            pieSeries1.InsideLabelColor = OxyColors.White;
        
            plotView.Model = plotModel1;

            SetNativeControl(plotView);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;
            if (e.PropertyName == XamarinCRM.CustomControls.BarChart.ItemsProperty.PropertyName)
            {

                var pieSeries1 = Control.Model.Series[0] as PieSeries;
                pieSeries1.Slices.Clear();
                foreach (var item in Element.Items)
                {
                    pieSeries1.Slices.Add(new PieSlice
                        {
                            Label = item.Name,
                            Value = item.Value
                        });
                }

                Control.Model.InvalidatePlot(true);
                Control.InvalidatePlot(true);

            }
        }
    }
}