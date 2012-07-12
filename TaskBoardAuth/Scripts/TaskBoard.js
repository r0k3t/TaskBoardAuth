$(document).ready(function() {
    $(".task").draggable();
    $(".taskColumn").resizable({ handles: "e" });

    $(".task").click(function() {
        $(this).remove();
    });
    $("#planning").droppable({
        drop: function(event, ui) {
            $("#planningStatusMsg").text("Task moved to planning. Top:" + ui.position.top.toString() + " Left: " + ui.position.left.toString());
            $(".task").draggable();
        }
    });
    $("#addNewTaskButton").click(function(evt) {
        evt.preventDefault();
        $('#newTaskDialog').dialog('open');

    });

    $('#saveNewTaskButton').click(function(evt) {
        evt.preventDefault();
        $.post("/TaskBoard/CreateTask", getTask(), function(result) {
            $("body").append("<div class='task' style='position: absolute; top: 12px; left: 3px;'><p>" + result.Name + "</p></div>");
            $(".task").draggable();
        });
        $('#newTaskDialog').dialog('close');
    });

    $('#newTaskDialog').dialog({
        title: "Add A New Task",
        autoOpen: false
    });

    function getTask() {
        var name = $("#taskName").val();
        var description = $("#taskDescription").val();
        var projectId = $('#projectId').text();
        return (name == "") ? null : {
            Name: name,
            Description: description,
            ProjectId: projectId,
            LocationTop: 100,
            LocationLeft: 120
        };
    }


});