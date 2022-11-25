
$( document ).ready(function() {
    //move popup menu to mouse pointer
    toolbar_pop_fontsize = document.getElementById('toolbar_fontSize_dropdown');
    if (toolbar_pop_fontsize) {
        toolbar_pop_fontsize.addEventListener('mouseover', function () {
            // Calculate and store some of the box coordinates:
            toolbar_fontsize = document.getElementById('toolbar_fontSize_container');
            modal = document.getElementById('toolbar_fontSize_popup_list_wrapper');
            modal.style.position = "absolute";
            modal.style.left = toolbar_fontsize.offsetLeft +  'px';
            modal.style.top =  toolbar_fontsize.getBoundingClientRect().top + toolbar_fontsize.offsetHeight  +  'px';
            modal.style.width =  toolbar_fontsize.offsetWidth +  'px';
            ul =  document.getElementById('toolbar_fontSize_popup');
            ul.style.width =  toolbar_fontsize.offsetWidth +  'px';
        });
    }
    toolbar_pop_fontfamily = document.getElementById('toolbar_fontfamily_dropdown');

    if (toolbar_pop_fontfamily) {
        toolbar_pop_fontfamily.addEventListener('mouseover', function () {
            // Calculate and store some of the box coordinates:
            toolbar_fontfamily = document.getElementById('toolbar_fontfamily_container');
            modal = document.getElementById('toolbar_fontFamily_popup_list_wrapper');
            modal.style.left = toolbar_fontfamily.offsetLeft +  'px';
            modal.style.top =  toolbar_fontfamily.getBoundingClientRect().top + toolbar_fontfamily.offsetHeight  +  'px';
            //modal.style.width =  toolbar_fontsize.offsetWidth +  'px';
            // ul =  document.getElementById('toolbar_fontfamily_popup');
            // ul.style.width =  toolbar_fontsize.offsetWidth +  'px';
        });
    }
    toolbar_pop_action = document.getElementById('toolbar_action');

    if (toolbar_pop_action) {
        toolbar_pop_action.addEventListener('mouseover', function () {
            // Calculate and store some of the box coordinates:
            toolbar_action = document.getElementById('toolbar_action');
            modal = document.getElementById('toolbar_action_popup_list_wrapper');
            modal.style.left = toolbar_action.offsetLeft - 4 + 'px';
            modal.style.top =  toolbar_action.getBoundingClientRect().top + toolbar_action.offsetHeight  +  'px';
            //modal.style.width =  toolbar_fontsize.offsetWidth +  'px';
            ul =  document.getElementById('toolbar_action_popup');
            ul.style.width =  toolbar_action.offsetWidth +  'px';
        });
    }
    toolbar_pop_communication = document.getElementById('toolbar_communication');

    if (toolbar_pop_communication) {
        toolbar_pop_communication.addEventListener('mouseover', function () {
            // Calculate and store some of the box coordinates:
            toolbar_communication = document.getElementById('toolbar_communication');
            modal = document.getElementById('toolbar_communication_popup_list_wrapper');
            modal.style.left = toolbar_communication.offsetLeft + 'px';
            modal.style.top =  toolbar_communication.getBoundingClientRect().top + toolbar_communication.offsetHeight  +  'px';
            //modal.style.width =  toolbar_fontsize.offsetWidth +  'px';
            ul =  document.getElementById('toolbar_communication_popup');
            ul.style.width =  toolbar_action.offsetWidth +  'px';
        });
    }

    toolbar_pop_zoom = document.getElementById('toolbar_zoom_dropdown');

    if (toolbar_pop_zoom) {
        toolbar_pop_zoom.addEventListener('mouseover', function () {
            // Calculate and store some of the box coordinates:
            toolbar_zoom = document.getElementById('toolbar_zoom_container');
            modal = document.getElementById('toolbar_zoom_popup_list_wrapper');
            modal.style.left = toolbar_zoom.offsetLeft +  'px';
            modal.style.top =  toolbar_zoom.getBoundingClientRect().top + toolbar_zoom.offsetHeight  +  'px';
            //modal.style.width =  toolbar_fontsize.offsetWidth +  'px';
            // ul =  document.getElementById('toolbar_fontfamily_popup');
            // ul.style.width =  toolbar_fontsize.offsetWidth +  'px';
        });
    }

    toolbar_pop_view = document.getElementById('toolbar_view_dropdown');

    if (toolbar_pop_view) {
        toolbar_pop_view.addEventListener('mouseover', function () {
            // Calculate and store some of the box coordinates:
            toolbar_view = document.getElementById('toolbar_view_container');
            modal = document.getElementById('toolbar_view_popup_list_wrapper');
            modal.style.left = toolbar_view.offsetLeft +  'px';
            modal.style.top =  toolbar_view.getBoundingClientRect().top + toolbar_view.offsetHeight  +  'px';
            //modal.style.width =  toolbar_fontsize.offsetWidth +  'px';
            // ul =  document.getElementById('toolbar_fontfamily_popup');
            // ul.style.width =  toolbar_fontsize.offsetWidth +  'px';
        });
    }


});
//Click Toolbar button
$('#toolbar_action').click(function(){
    $('#toolbar_action_popup_list_wrapper').fadeIn();
});

$('#toolbar_action_popup_list_wrapper li').click(function(){
    $('#toolbar_action_popup_list_wrapper').fadeOut();
});

$('#toolbar_communication').click(function(){
    $('#toolbar_communication_popup_list_wrapper').fadeIn();
});

$('#toolbar_communication_popup_list_wrapper li').click(function(){
    $('#toolbar_communication_popup_list_wrapper').fadeOut();
});

$('#toolbar_zoom_dropdown').click(function(){
    $('#toolbar_zoom_popup_list_wrapper').fadeIn();
});

$('#toolbar_zoom_popup_list_wrapper li').click(function(){
    $('#toolbar_zoom_popup_list_wrapper').fadeOut();
});

$('#toolbar_view_dropdown').click(function(){
    $('#toolbar_view_popup_list_wrapper').fadeIn();
});

$('#toolbar_view_popup_list_wrapper li').click(function(){
    $('#toolbar_view_popup_list_wrapper').fadeOut();
});

$('#toolbar_fontSize_dropdown').click(function(){
    $('#toolbar_fontSize_popup_list_wrapper').fadeIn();
});

$('#toolbar_fontSize_popup_list_wrapper li').click(function(){
    $('#toolbar_fontSize_popup_list_wrapper').fadeOut();
});

$('#toolbar_fontfamily_dropdown').click(function(){
    $('#toolbar_fontFamily_popup_list_wrapper').fadeIn();
});

$('#toolbar_fontFamily_popup_list_wrapper li').click(function(){
    $('#toolbar_fontFamily_popup_list_wrapper').fadeOut();
});

$('#toolbar_layout').click(function(){
    $('#model-container').css("display",'none');
    $('#task-list-container').css("display",'none');
    $('#defaultTab').fadeIn();
   
});

//Click Popup up menu
document.addEventListener('click',function(event)
{
    toolbar_pop_fontsize_1 = document.getElementById('toolbar_fontSize_popup_list_wrapper');
    toolbar_pop_fontsize_2 = document.getElementById('toolbar_fontSize_wrapper');
    if(!toolbar_pop_fontsize_1.contains(event.target) && !toolbar_pop_fontsize_2.contains(event.target))
    {
        $('#toolbar_fontSize_popup_list_wrapper').fadeOut();
    }

    var toolbar_pop_fontfamily_1 = document.getElementById('toolbar_fontFamily_popup_list_wrapper');
    var toolbar_pop_fontfamily_2 = document.getElementById('toolbar__fontfamily');
    if(!toolbar_pop_fontfamily_1.contains(event.target) && !toolbar_pop_fontfamily_2.contains(event.target))
    {
        $('#toolbar_fontFamily_popup_list_wrapper').fadeOut();
    }

    var toolbar_pop_action_1 = document.getElementById('toolbar_action_popup_list_wrapper');
    var toolbar_pop_action_2 = document.getElementById('toolbar_action');
    if(!toolbar_pop_action_1.contains(event.target) && !toolbar_pop_action_2.contains(event.target))
    {
        $('#toolbar_action_popup_list_wrapper').fadeOut();
    }

    var toolbar_pop_action_1 = document.getElementById('toolbar_communication_popup_list_wrapper');
    var toolbar_pop_action_2 = document.getElementById('toolbar_communication');
    if(!toolbar_pop_action_1.contains(event.target) && !toolbar_pop_action_2.contains(event.target))
    {
        $('#toolbar_communication_popup_list_wrapper').fadeOut();
    }

    var toolbar_pop_zoom_1 = document.getElementById('toolbar_zoom_popup_list_wrapper');
    var toolbar_pop_zoom_2 = document.getElementById('toolbar_zoom');
    if(!toolbar_pop_zoom_1.contains(event.target) && !toolbar_pop_zoom_2.contains(event.target))
    {
        $('#toolbar_zoom_popup_list_wrapper').fadeOut();
    }
    
    var toolbar_pop_layout_1 = document.getElementById('defaultTab');
    var toolbar_pop_layout_2 = document.getElementById('toolbar_layout');
    if(!toolbar_pop_layout_1.contains(event.target) && !toolbar_pop_layout_2.contains(event.target))
    {
        $('#defaultTab').css("display",'none');
        $('#model-container').fadeIn();
        $('#task-list-container').fadeIn();
    }

    var toolbar_pop_view_1 = document.getElementById('toolbar_view_popup_list_wrapper');
    var toolbar_pop_view_2 = document.getElementById('toolbar_view');
    if(!toolbar_pop_view_1.contains(event.target) && !toolbar_pop_view_2.contains(event.target))
    {
        $('#toolbar_view_popup_list_wrapper').fadeOut();
    }
});

//mousescroll event
document.addEventListener('wheel', (e) => 
{
    $('#toolbar_fontSize_popup_list_wrapper').fadeOut();
    $('#toolbar_fontFamily_popup_list_wrapper').fadeOut();
    $('#toolbar_action_popup_list_wrapper').fadeOut();
    $('#toolbar_communication_popup_list_wrapper').fadeOut();
    $('#toolbar_zoom_popup_list_wrapper').fadeOut();
    $('#toolbar_view_popup_list_wrapper').fadeOut();
});