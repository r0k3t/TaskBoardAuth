$(document).ready(function () {
    $(".task").draggable();
    $(".taskColumn").resizable({ handles: "e" });

    //$(".task").click(function() {
    //    $(this).remove();
    //});

    $("#planning").droppable({
        drop: function (event, ui) {
            var droppedElement = ui.draggable;
            var dropResult = getUpdateInformationFromDroppedElement(droppedElement, ui.position.top, ui.position.left, "Design");
            if (is('String', dropResult))
                alert(dropResult);
            else
                $.post("/TaskBoard/UpdateTaskStatus", dropResult, function (result) {
                    $(".task").draggable();
                });
        }
    });


    $("#addNewTaskButton").click(function (evt) {
        evt.preventDefault();
        $('#newTaskDialog').dialog('open');

    });

    $('#saveNewTaskButton').click(function (evt) {
        evt.preventDefault();
        $.post("/TaskBoard/CreateTask", getNewTask(), function (result) {
            $("body").append("<div class='task' style='position: absolute; top: 190px; left: 12px;'><p>" + result.Name + "</p></div>");
            $(".task").draggable();
        });
        $('#newTaskDialog').dialog('close');
    });

    $('#newTaskDialog').dialog({
        title: "Add A New Task",
        autoOpen: false
    });

    function getNewTask() {
        var name = $("#taskName").val();
        var description = $("#taskDescription").val();
        var projectId = $('#projectId').text();
        return (name == "") ? null : {
            Name: name,
            Description: description,
            ProjectId: projectId
        };
    }


});

function updateTaskStatus(task) {
    var returnMessage = "";
    if (task === null) {
        returnMessage = "Task was null";
        return returnMessage;
    }
    $.post("/TaskBoard/UpateTask", task, function (result) {
        returnMessage = result.name;
    });
    return returnMessage;
}

function getUpdateInformationFromDroppedElement(element, positionTop, positionLeft, status) {
    var taskId = $(element.children('span')[0]).attr('id');
    var task = {
        TaskId: taskId,
        LocationTop: positionTop,
        LocationLeft: positionLeft,
        TaskStatus: status
    };
    if (task.TaskId === null)
        return "TaskId was null";
    if (task.LocationLeft === null)
        return "LocationLeft was null";
    if (task.LocationTop === null)
        return "LocationTop was null";
    if (task.TaskStatus === null)
        return "TaskStatus was null";
    return task;
}

function is(type, obj) {
    var clas = Object.prototype.toString.call(obj).slice(8, -1);
    return obj !== undefined && obj !== null && clas === type;
}
