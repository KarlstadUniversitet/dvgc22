
var calheader_class = "calheader-float";
var ishidden = false;
var isOnlyBreaksShown = false;
var isAntiSchedule = false;

$(window).scroll(function (e) {
    scroll_header();
    stop_scroll_header();
    scroll_week_number();
    stop_scroll_week_number();
});


function scroll_week_number() {
    var page = $(this);
    jQuery.each($('.div_week'), function () {
        if (page.scrollTop() > $(this).position().top && $(this).css('position') != 'fixed') {
            $(this).addClass("div_week_float");
        }
    });
    }

function stop_scroll_week_number() {
    var page = $(this);
    jQuery.each($('.div_week'), function () {
        if ((page.scrollTop() > ($(this).closest('td').position().top + $(this).closest('td').height() - 25) || page.scrollTop() < $(this).closest('td').position().top) && $(this).css('position') == 'fixed') {
            $(this).removeClass("div_week_float");
        }
    });
}

function scroll_header() {
    if ($(this).scrollTop() > 250 && $('#calheader').css('position') != 'fixed') {
        $('#calheader').removeClass("calheader-top");
        $('#calheader').addClass(calheader_class);
    }
}

function stop_scroll_header() {
    if ($(this).scrollTop() < 250 && $('#calheader').css('position') == 'fixed') {
        $('#calheader').removeClass(calheader_class);
        $('#calheader').addClass("calheader-top");
    }
}

window.onload = function () {
}

/*---------------------------------------*/
/* ------ Local storage functions ------- */
/*---------------------------------------*/

function supports_html5_storage()
{
    try {
        return 'localStorage' in window && window['localStorage'] !== null;
    } catch (e) {
        return false;
    }
}

function save_course_code(appcode) {
    var url = "/Category/AddCourseToCategory";
    $('#addcoursefromsearch :input:checked').each(function () {
        $.ajax({
            type: "POST",
            url: url,
            data: { categoryId:$(this).val(), applicationCode: appcode},
            success: function (data) {
                store_to_local_storage(data);
                refresh_categories();
            }
        });
    });
}

function show_hide_searchholder() {
    if (ishidden) {
        show_searchholder();
        ishidden = false;
        $('.showhidebutton').html('<img id="showhide" src="/Content/Images/hide_search.png" onclick="show_hide_searchholder()">');
    } else {
        hide_searchholder();
        ishidden = true;
        $('.showhidebutton').html('<img id="showhide" src="/Content/Images/show_search.png" onclick="show_hide_searchholder()">');
    }
}

function show_searchholder() {
    $('#content').width("70%");

    $('#searchholder').css('display', 'inline-block');
    $('#searchholder').width("auto");
    calheader_class = "calheader-float-small";
}

function hide_searchholder() {
    $('#searchholder').hide();
    $('#content').width("95%");
    stop_scroll_header();
    stop_scroll_week_number();
    calheader_class = "calheader-float";
}

$('#searchbutton').click(function () {
    var url = "/Search/Search";
    var searchValue = $('#SearchWord').val();
        $.ajax({
            type: "POST",
            url: url,
            data: { SearchWord: searchValue },
            success: function (data) {
                //console.log(searchValue);
                //console.log(data);
                $('#searchholder').html(data);
                //$('.showhidebutton').html('<a href="#" id="showhide" onclick="show_hide_searchholder()">> H i d e S e a r c h ></a>');
                $('.showhidebutton').html('<img id="showhide" src="/Content/Images/hide_search.png" onclick="show_hide_searchholder()">');
                show_searchholder();
            }
        });
    return false;
});

     

function save_bool_true() {
    localStorage["bool"] = true;
}

function save_bool_false() {
    localStorage["bool"] = false;
}

function default_schedule_type() {
    return localStorage.getItem("bool");
}
function hide_div_with(color)
{
    if (color != "lectureBreak") {
        $('.' + color + 'togg').toggle();
        $('.codeColor ' + color).toggleClass('grayedout');
        $('.codeColor').each(function () {
            if ($(this).hasClass(color))
                $(this).toggleClass('grayedout');
        });
        console.log('.codeColor ' + color);
    }
    else 
        $('.' + color + 'togg').toggle();
    
}

function store_to_local_storage(jsonObject) {
    localStorage.setItem(local_storage_key, jsonObject);
}

function read_from_local_storage(jsonObject) {
    if (localStorage[local_storage_key] != null)
        return localStorage.getItem(local_storage_key);
    return "";
}


function refresh_categories() {
    $('#cssmenu').load('/Category/');
    //$('#searchholder').load('/Search/');
    setTimeout(function () { $('.popbox').popbox() }, 500);
    return false;
}



/************************************************************************************************
    Functions hiding and showing breaks
*************************************************************************************************/
    function getisOnlyBreaksShown(){return isOnlyBreaksShown;}

    function toggle_breaks() {
        if (isOnlyBreaksShown) {
            showEverythingButBreaks();
            isOnlyBreaksShown = false;
        }
        else {
            isOnlyBreaksShown = true;
            showOnlyBreaks();
        }
    }

    function toggle_back() {
        if (isOnlyBreaksShown) {
            showEverythingButBreaks();
            isOnlyBreaksShown = false;}
    }

    function showOnlyBreaks() {
        $('.popout').each(function () {
            if (!$(this).hasClass('lectureBreak'))
                $(this).hide();
        });
        $('.lectureBreak').show();
        $('.codeColor').addClass('grayedout');
        $('#breaks').text("Show everything but breaks");
    }

    function showEverythingButBreaks() {
        $('.popout').each(function () {
            if (!$(this).hasClass('lectureBreak'))
                $(this).show();
        });
        $('.lectureBreak').hide();
        $('.codeColor').removeClass('grayedout');
        $('#breaks').text("Only show breaks");
    }



    /************************************************************************************************
        Function related to remove, add, manipulate categories and local storage
    *************************************************************************************************/

    var local_storage_key = "catKey";

    function post_from_local_storage() {
        var url = "/Category/SetCategoriesDeserialized";
        $.ajax({
            type: "POST",
            url: url,
            data: { jsonNotation: read_from_local_storage() },
            success: function (data) {
                store_to_local_storage(data);
                $("#cssmenu").load("/Category/Index");
            }
        });
    
        return false;
    }

    function remove_course_from_category(catID, appCode) {
        var url = '/Category/RemoveCourseFromCategoryWithID';
        $.ajax({
            type: "POST",
            url: url,
            data: { categoryID: catID, applicationCode: appCode },
            success: function (data) {
                store_to_local_storage(data);
                refresh_categories();
            },
            error: function (data) {
                console.log("Error: \n" + data);
            }
        });
        return false;
    }

    function remove_category(catName) {
        var url = '/Category/RemoveCategory';
        $.ajax({
            type: "POST",
            url: url,
            data: { Name: catName },
            success: function (data) {
                refresh_local_storage();
                refresh_categories();
            },
            error: function (data) {
            }
        });
        return false;
    }

function refresh_local_storage(callback) {
        var url = "/Category/GetCategoriesSerialized";
        var searchValue = "";

        $.ajax({
            type: "POST",
            url: url,
            data: {},
            success: function (data) {
                store_to_local_storage(data);
            if (typeof (callback) == "function") {
                $("#cssmenu").load("/Category/Index", callback);
            } else {
                $("#cssmenu").load("/Category/Index");
            }
        }
        });
    
    }




    /* Add the events */
    addEvent(window, 'load', dynamicLayout);
    addEvent(window, 'resize', dynamicLayout);
    var scrollTime = 7500;


    /************************************************************************************************
        Function related to loading course code
    *************************************************************************************************/
    function load_course(coursecodes, mode) {
        var url = "/Calendar/Partial" + mode + "/";
    if (presentationModeActivated) {
        $.ajax({
            type: "POST",
            url: url,
            data: { applicationCode: coursecodes },
            success: function (data) {
                scrollStop();
                $('#content').html(data);
                animationIn();
                hideButtons();
                enableExitButton();
                if (presentationModeCalender == "Agenda") {
                    $('#agendaMode').click();
                }
                
            }

        });
	}
    else {
        $('#content').html("<div id='loading'></div>");
        $.ajax({
            type: "POST",
            url: url,
            data: { applicationCode: coursecodes },
            success: function (data) {
                $('#content').html(data);
            }
        });
    }
        if (mode.indexOf("Month")!=-1)
            save_bool_false();
        else
            save_bool_true();
        return false;
    }

    function load_coursecode(coursecode)
    {
        //console.log(coursecode);
        var staticMode;
        if (localStorage["bool"] == "true")
            staticMode = "Agenda";
        else
            staticMode = "Month";

        load_course(coursecode, staticMode);
        return false;
    }

    function switchMode()
    {
        if (localStorage["bool"] == "true")
        {
            $('#calendarMode').click();
        }
        else
        {
            $('#agendaMode').click();
        }
    }


    /************************************************************************************************
        Functions related to dynamic CSS
    *************************************************************************************************/

    /* Browser resize event functions */
    function getBrowserWidth() {
        if (window.innerWidth) {
            return window.innerWidth;
        }
        else if (document.documentElement && document.documentElement.clientWidth != 0) {
            return document.documentElement.clientWidth;
        }
        else if (document.body) { return document.body.clientWidth; }
        return 0;
    }

    /* Change they layout of the page */
    function dynamicLayout() {
        var browserWidth = getBrowserWidth();

        if (browserWidth < 750) {
            changeLayout("thin");
        }
        if ((browserWidth >= 750) && (browserWidth <= 950)) {
            changeLayout("wide");
        }
        if (browserWidth > 950) {
            changeLayout("wider");
        }
    }

    /* Set the new stylesheet */
    function changeLayout(description) {
        if (description == "thin") {
            $("#size-stylesheet").attr("href", "/Content/Site.css");
        }
        else if (description == "wide") {
            $("#size-stylesheet").attr("href", "/Content/Site.css");
        }
        else {
            $("#size-stylesheet").attr("href", "/Content/Site.css");
        }
    }

    /* Function to add the window resize event */
    function addEvent(obj, type, fn) {
        if (obj.addEventListener) {
            obj.addEventListener(type, fn, false);
        }
        else if (obj.attachEvent) {
            obj["e" + type + fn] = fn;
            obj[type + fn] = function () { obj["e" + type + fn](window.event); }
            obj.attachEvent("on" + type, obj[type + fn]);
        }
    }
    function show_add_course()
    {
        if ($(".regular-checkbox").is(':checked'))
        {
            $("#course_added").html('<div class="course_added">Course added</div>');
            $(".course_added").show();
            $(".course_added").fadeOut(2000);
        }
        else{
            $("#course_added").html('<div class="course_added">Please choose categories</div>');
            $(".course_added").show();
            $(".course_added").fadeOut(2000);
        }
    }

    function toggle_breaks() {
        if (isOnlyBreaksShown) {
            showEverythingButBreaks();
            isOnlyBreaksShown = false;
        }
        else {
            isOnlyBreaksShown = true;
            showOnlyBreaks();
        }
    }

    function showOnlyBreaks() {
        $('.toggleable').each(function () {
            if (!$(this).hasClass('lectureBreak'))
                $(this).hide();
        });
        $('.lectureBreak').show();
        $('.codeColor').addClass('grayedout');
        $('#breaks').text("Show everything but breaks");
    }

    function showEverythingButBreaks() {
        $('.toggleable').each(function () {
            if (!$(this).hasClass('lectureBreak'))
                $(this).show();
        });
        $('.lectureBreak').hide();
        $('.codeColor').removeClass('grayedout');
        $('#breaks').text("Only show breaks");
    }



    /* Add the events */
    addEvent(window, 'load', dynamicLayout);
    addEvent(window, 'resize', dynamicLayout);

    function enableExitButton()
    {
        $("#exitButton").css('visibility', 'visible');
        $("#exitButton").mouseover(function () {
            $("#exitButton").css("opacity", "1");
        });
        $("#exitButton").mouseout(function () {
            $("#exitButton").css("opacity", "0");
        });
    }
    function disableExitButton() {
        $("#exitButton").css('visibility', 'hidden');
    }

    //Exits presentation mode
    function exitButtonClick() {
        showButtons();
        disableExitButton();
        stopPresentationMode();
    }

    function hideButtons() {
        $('#header').hide();
        $('#buttonOptions').hide();
        $('#cssmenu').hide();
        $("#searchholder").hide();
        $("#footer").hide();
        $(".showhidebutton").hide();
        $('body').css('overflow', 'hidden');
    }

    function showButtons() {
        $('#header').show();
        $('#buttonOptions').show();
        $('#cssmenu').show();
        $("#searchholder").show();
        $(".showhidebutton").show();
        $('body').css('overflow', 'visible');
    }

    function checkTimebox() {
        if ($('#timeTextbox').val() == "") {
            $('#timeTextbox').attr("value", "");
            $("#timeTextbox").fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
            return false;
        }
        else if (!$.isNumeric($('#timeTextbox').val())) {
            $('#timeTextbox').attr("value", "");
            $("#timeTextbox").fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
            return false;
        }
        else if ($.isNumeric($('#timeTextbox').val()) && $('#timeTextbox').val() < 1) {
            $('#timeTextbox').attr("value", "");
            $("#timeTextbox").fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
            return false;
        }

        return true;
    }

    function checkTransitionOption() {
        if ($('#TransitionOption').val() == "") {
            $("#TransitionOption").fadeOut(100).fadeIn(100).fadeOut(100).fadeIn(100);
            return false;
        }

        return true;
    }

    function fullscreenMode()
    {
        if (!(checkTimebox() && checkTransitionOption()))
            return;
        hideButtons();
        checkAntiSchedule();
        startPresentationMode(($('#timeTextbox').val()) * 1000);
        enableExitButton();
    }

    function showCategory(index)
    {
        console.log("index is " + index);
        $('.active').removeClass("active");
        $('.has-sub .callink').each(function (i)
        {
            if (index == i)
            {
                this.click();
            }
        });
    }

    function nextCategory()
    {
        var activeCount = 0;
        var activeIndex;
        $('.has-sub .callink').each(function (index)
        {
            if ($(this).hasClass("callink active"))
            {
                activeIndex = index;
                activeCount++;
            }
        });
        if (activeCount > 1)
        {
            console.error("More than one category was active");
        }
        else if (activeCount == 0)
        {
            save_bool_false();
            $('.callink').first().click();
        }
        else
        {
            save_bool_false();
            showCategory((activeIndex + 1) % ($('.has-sub .callink').size()));
        }
    }

var animationType = "fade";

function animationOut() {
    switch (animationType) {
        case "fade":
            $('#content').fadeOut(1000, function () {
                presentationModeSwitch();
            });
            break;
        case "slide":
            $("#content").hide({ effect: "slide", direction: "right" });
            presentationModeSwitch();
            break;
        case "fold":
            $("#content").hide({ effect: "fold", duration: 1000, size: 150 });
            presentationModeSwitch();
            break;
        case "scale":
            $("#content").hide({ effect: "scale", duration: 1000 });
            presentationModeSwitch();
            break;
        case "bounce":
            $("#content").hide({ effect: "bounce", times: 3 }, function () {
                presentationModeSwitch();
            });
            break;
        default:
            $('#content').fadeOut(1000, function () {
                presentationModeSwitch();
            });
            break;
    }
}

function animationIn() {

    switch (animationType) {
        case "fade":
            $('#content').fadeIn(1000, function () {
                scrollDown(scrollTime);
            });
            break;
        case "slide":
            $("#content").show({ effect: "slide", direction: "left" });
            scrollDown(scrollTime);
            break;
        case "fold":
            $("#content").show({ effect: "fold", duration: 1000, size: 150 });
            scrollDown(scrollTime);
            break;
        case "scale":
            $("#content").show({ effect: "scale", duration: 1000 });
            scrollDown(scrollTime);
            break;
        case "bounce":
            $("#content").show({ effect: "slide", direction: "up" });
            scrollDown(scrollTime);
            break;
        default:
            $('#content').fadeIn(1000, function () {
                scrollDown(scrollTime);
            });
            break;
    }
}

function presentationModeSwitch() {
    if (presentationModeCalender == "Both") {
        if (switched) {
            nextCategory();
            switched = false;
        }
        else {
            switchMode();
            switched = true;
        }
    }
    else {
        nextCategory();
    }
}

var presentationModeCalender = "Both";
var presentationModeTimer;
var presentationModeActivated = false;
var switched = false;

function startPresentationMode(time) {
    assert(!presentationModeActivated, "Presentation mode already started");
    presentationModeActivated = true;
    scrollTime = time;
    scrollDown(time);
    animationType = $("#TransitionOption").val();
    whereToStart();
    presentationModeTimer = setInterval(
        function()
        {
            animationOut();
            }
            , time);
    }


    function whereToStart() {

        if ($('#AgendaRadio').is(':checked')) {
            $('#agendaMode').click();
            presentationModeCalender = "Agenda";
            if (isAntiSchedule == true) { showOnlyBreaks(); }
            else { showEverythingButBreaks(); }
        }
        else if ($('#CalenderRadio').is(':checked')) {
            $('#calendarMode').click();
            presentationModeCalender = "Calender";
            if (isAntiSchedule == true) { showOnlyBreaks(); }
            else { showEverythingButBreaks(); }
        }
        else if ($('#BothRadio').is(':checked')) {
            $('#calendarMode').click();
            presentationModeCalender = "Both";
            if (isAntiSchedule == true) { showOnlyBreaks(); }
            else { showEverythingButBreaks(); }
        }
    }

    function stopPresentationMode()
    {
        scrollStop();
        window.scrollTo(0, 0);
        switched = false;
        presentationModeActivated = false;
        clearInterval(presentationModeTimer);
        presentationModeTimer = undefined;
        presentationCategoryTimer = undefined;
        presentationTimeout = undefined;
    }

    function assert(bool,expression)
    {
        if (!bool)
        {
            throw new Error(expression);
        }
    }

    function textBoxForShareButton()
    {

        if ($('#sharedTextbox').css('visibility') == 'visible') 
        {
            $('#sharedTextbox').css('visibility', 'hidden');
        }
        else
        {
            $.ajax(
            {
                url: "HTTP://"+location.host + "/Share",
                dataType: "text"
            }).done(function (text) {
            refresh_local_storage(function () {
                $('#sharedTextbox').css('visibility', 'visible');
                $('#sharedBox').val(text);
                $("#sharedBox").select();
                $('.popbox').popbox();
                startUpdating();
            });
          
        });
        
        }
    
    }

    function generateURL()
    {

    }

    function shareModeActivated() {
        if (categoryHandler.shareId != null)
            return true;
        else
            return false;
    }

    function scrollStop() {
        $('html, body').stop();
    }
    function scrollUp(time) {
        console.log("starting rolling up");
        $('html, body').animate({
            scrollTop: $("#top").offset().top
        }, time, "linear");
    }
    function scrollDown(time) {
        window.scrollTo(0, 0);
        console.log("starting rolling down");
        $('html, body').animate({
            scrollTop: $("#footer").offset().top
        }, time, "linear");
        if (isAntiSchedule == true) { showOnlyBreaks(); }
    }

    var categoryHandler;
function startUpdating() {
        categoryHandler = JSON.parse(localStorage[local_storage_key]);
    if (categoryHandler.shareId != null) {
        setInterval(checkForUpdates, 5000);
        $("#labelShareMode").text("Shared ID is:" + categoryHandler.shareId);
    }

}

function checkForUpdates()
{
    $.ajax(
        {
            url: "HTTP://" + location.host + "/Share/UpdateCategoryHandler",
            dataType: "text"
        }).done(function (text) {
            if (text == "update") {
                refresh_categories();
            }

        });
}

    function optionsPopup() {


    }

function checkAntiSchedule() {
    if ($('#antiSchedule').is(':checked')) {
        console.log("Anti schedule is checked");
        isAntiSchedule = true;
    }
    else {
        console.log("Anti schedule is not checked");
        isAntiSchedule = false;
    }

    }
function test() {
    console.log(localStorage[local_storage_key]);
}