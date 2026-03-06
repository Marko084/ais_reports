/*
 Fint Theme v0.0.3
 FusionCharts JavaScript Library

 Copyright FusionCharts Technologies LLP
 License Information at <http://www.fusioncharts.com/license>
*/

FusionCharts.register("theme", {
    name: "convergent",
    theme: {
        base: {
            chart: {
                paletteColors: "#002d64,#123900,#437abd,#a8c59b",
                captionFontSize: "14",
                subCaptionFontSize: "12",
                captionFontBold: "1",
                subCaptionFontBold: "0",
                showHoverEffect: "1",
                placeValuesInside: "0",
                baseFontColor: "#272727 !important",
                bgColor: "#ffffff !important"
              },
                trendlines: [{
                    color: "#FF000",
                    thickness: "3",
                    dashed: "1",
                    dashLen: "4",
                    dashGap: "2"
                }]
              },
            column2d: {
                    dataset: {
                        data: function(dataObj) {
                                color: (Number(dataObj.value) < 0 ? "#3333FF" : "#CC0000")
                                }
                    }
            },
            bubble: {
                    chart: {
                        drawQuadrant: "1",
                        quadrantLineColor: "3",
                        quadrantLineThickness: "1",
                        quadrantLineAlpha: "4",
                        },
                dataset: [{
                    regressionLineColor: "#123456",
                    regressionLineThickness: "3",
                    regressionLineAlpha: "70"  
                }]
            },
            pie2d: {
                    chart: {
                        showPercentInToolTip: "1",
                        enableSmartLabels: "1"
                    }
            },
            zoomline: {
                    chart: {
                        anchorMinRenderDistance : "20"				
                    }
            },		
            gantt: {
                    processes: [{
                        headerFont: "Arial",
                        headerFontSize: "16",
                        headerFontColor: "#321ABC",
                        headerIsBold: "1",
                        headerIsUnderline: "1",
                        headerAlign: "left",
                        headerVAlign: "bottom"
                    }]
            },      
geo: {
        chart: {
            showLabels: "1",
            useSNameInLabels: "1",
            useSNameInToolTip: "0",
            entityFillHoverColor: "#9A9A9A",
            entityFillHoverAlpha: "60",
            markerFillHoverColor: "#8AE65C",
            markerFillHoverAlpha: "60",
            },
        marker: {
                connector: {
                    thickness: "4",
                    color: "#336699",
                    alpha: "60",
                    dashed: "1",
                    dashLen: "4",
                    dashGap: "2"
                }
        }
    }
   }
});
