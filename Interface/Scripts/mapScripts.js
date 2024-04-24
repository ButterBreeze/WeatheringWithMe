let map;
let temperatureDataTable;
let precipitationChanceDataTable;
let isTemperatureOptionSelected;


// start loading the chart libraries here so they load in time
google.charts.load('current');


function handleClick(/* MouseEvent */ e) {
    //clear the latest error if there is to avoid confusion
    clearError();
    
    // weather api only accepts coords to 4 decimal places, so shorten them here and convert to string
    let Coordinates =
        {
            "Latitude" : `${e.latLng.lat()}`,
            "Longitude" : `${e.latLng.lng()}`
        }
        
    $.ajax({
        dataType: "json",
        url: `/Home/GetWeatherForecast`,
        type: 'POST',
        data: '{ coords : ' + JSON.stringify(Coordinates) + '}',
        contentType: "application/json; charset=utf-8",
        success: function (weatherForecast){
            populateCurrentData(weatherForecast);
            populateChartData(weatherForecast)
        },
        error: function (error){
            console.log(error);
            $("#error-text").text(`Error: ${error.statusText}`);
            $("#error-banner").css("visibility", "visible");
        }
    });
}

function clearError()
{
    $("#error-text").text("");
    $("#error-banner").css("visibility", "hidden");
}

function populateCurrentData(weatherForecast)
{
    $("#current-temp").text(`${weatherForecast.FirstPeriod.Temperature} °${weatherForecast.FirstPeriod.TemperatureUnit}`);
    $("#todays-low-temp").text(`${weatherForecast.TodaysLowTemp} °${weatherForecast.FirstPeriod.TemperatureUnit}`);
    $("#todays-high-temp").text(`${weatherForecast.TodaysHighTemp} °${weatherForecast.FirstPeriod.TemperatureUnit}`);
}

async function initMap(centerLat, centerLng, zoom, mapId) {
    const { Map } = await google.maps.importLibrary("maps");
    map = new Map(document.getElementById("main-map"), {
        center: { lat: centerLat, lng: centerLng },
        zoom: zoom,
        mapId: mapId,
        mapTypeControl: false,
    });

    map.addListener("click", handleClick);
}
function initChartForSideBar()
{
    temperatureDataTable = new google.visualization.DataTable();
    temperatureDataTable.addColumn('number', 'Hour');
    temperatureDataTable.addColumn('number', 'Temperature(°F)');

    precipitationChanceDataTable = new google.visualization.DataTable();
    precipitationChanceDataTable.addColumn('number', 'Hour');
    precipitationChanceDataTable.addColumn('number', 'Percent Chance');

    // setup the initial chart as temperature, then when the weather detail is changed, 
    // redraw with the selected options's dataTable

    isTemperatureOptionSelected = true;
    google.charts.setOnLoadCallback(drawChart);
}

function drawChart()
{

    let dataTable;
    let options;
    if(isTemperatureOptionSelected)
    {
        dataTable = temperatureDataTable;
        options = {
            title: 'Temperature',
            legend: {position:'none'},
            annotations: {style: 'line'}
        };
    } else{
        dataTable = precipitationChanceDataTable;
        options = {
            title: 'Precipitation Chance',
            legend: {position:'none'},
            annotations: {style: 'line'}
        };
    }
    let wrapper = new google.visualization.ChartWrapper({
        chartType: 'LineChart',
        dataTable: dataTable,
        options: options,
        containerId: 'forecast-chart'
    });
    wrapper.draw();
}

function populateChartData(weatherForecast)
{
    temperatureDataTable = new google.visualization.DataTable();
    temperatureDataTable.addColumn('string', 'Hour');
    temperatureDataTable.addColumn({type: 'string', role: 'annotation'});
    temperatureDataTable.addColumn('number', `Temperature(°${weatherForecast.FirstPeriod.TemperatureUnit})`);

    precipitationChanceDataTable = new google.visualization.DataTable();
    precipitationChanceDataTable.addColumn('string', 'Hour');
    precipitationChanceDataTable.addColumn({type: 'string', role: 'annotation'});
    precipitationChanceDataTable.addColumn('number', 'Percent Chance');
    
    $.each(weatherForecast.ForecastPeriods, function(index, period)
    {
        if(period.DisplayStartTime === '12 AM')
        {
            temperatureDataTable.addRow([period.DisplayStartTime, "" , period.Temperature]);
            precipitationChanceDataTable.addRow([period.DisplayStartTime, "",period.ProbabilityOfPrecipitation.Value])
        }
        temperatureDataTable.addRow([period.DisplayStartTime, null, period.Temperature]);
        precipitationChanceDataTable.addRow([period.DisplayStartTime, null, period.ProbabilityOfPrecipitation.Value])
    })

    drawChart();
}

$('#forecast-weather-option').change(function(){
    isTemperatureOptionSelected = $(this).val() === 'temperature';
    drawChart();
});

// google api for the charts needs to load, so waiting until everything is loaded
$(window).on('load', function() {
    initChartForSideBar();
});