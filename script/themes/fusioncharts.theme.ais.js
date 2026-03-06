/*
 Fint Theme v0.0.3
 FusionCharts JavaScript Library

 Copyright FusionCharts Technologies LLP
 License Information at <http://www.fusioncharts.com/license>
*/

FusionCharts.register("theme", {
    name: "ais",
    theme: {
        base: {
            chart: {
                paletteColors: "#2f5b8f,#ff8c00,#92312d,#78923e,#5c4479,#e0cb2a",
                captionFontSize: "14",
                subCaptionFontSize: "12",
                captionFontBold: "1",
                subCaptionFontBold: "0",
                showHoverEffect: "1",
                baseFontColor: "#ffffff",
                bgColor: "#000000 !important",
                toolTipColor: "#ffffff",
                toolTipBorderThickness: "0",
                toolTipBgColor: "#444444",
                toolTipBorderRadius: "3",
                toolTipPadding: "5",
                placeValuesInside: "1",
                rotateValues: "1",
                legendBgAlpha: "0",
                legendBorderAlpha: "0",
                legendShadow: "0",
                legendItemFontSize: "10",
                legendItemFontColor: "#ffffff",
                legendNumColumns: "3",
                canvasBgAlpha: "0",
                bgColor: "000000,00000",
                bgRatio: "50,50",
                bgAlpha: "100,100",
                divLineColor: "#444444",
                divLineAlpha: "100",
                xAxisNameFontSize: "18",
                yAxisNameFontSize: "18",
                xAxisNameFontBold: "1",
                yAxisNameFontBold: "1",
                exportEnabled: "1"
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
                                color: (Number(dataObj.value) < 0 ? "#000000" : "#000000")
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
            pyramid: {
                chart: {
                    borderColor: "#000000",
                    showBorder: "0"
                }
            },
            funnel: {
                chart: {
                    borderColor: "#000000",
                    showBorder: "0"
                }
            },
            pie2d: {
                    chart: {
                        showPercentInToolTip: "1",
                        enableSmartLabels: "1"
                    }
            },
            pie3d: {
                chart: {
                    showPercentInToolTip: "1",
                    enableSmartLabels: "0"
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
