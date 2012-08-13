$(document).ready(function () {

    var projectCreationOutcome = $("#projectCreationOutcome");
    $("#addNewProjectLink").click(function (evt) {

        evt.preventDefault();
        var projectError = checkForNewProjectError();
        if (projectError != null) {
            projectCreationOutcome.addClass("message-error");
            projectCreationOutcome.removeClass("message-success");
            projectCreationOutcome.text(projectError);
        } else {
            var project = getProject();
            $.post("/TaskBoard/CreateProject", project, function (result) {
                setProjectCreatedMessege(result);
            });
        }
    });

    $('.projectAdminLinks a').hover(
        function () {
            showToolTip('#editProjectToolTip', this);
        },
        function () {
            hideToolTip('#editProjectToolTip', this);
        });

    $('img[id^="showDetails_"]').hover(
        function () {
            showToolTip('#ShowDescriptionToolTip', this);
        },
        function () {
            hideToolTip('#ShowDescriptionToolTip', this);
        });

    $('img[id^="closeProject_"]').hover(
        function () {
            showToolTip('#closeToolTip', this);
        },
        function () {
            hideToolTip('#closeToolTip', this);
        });


    function hideToolTip(toolTip) {
        $(toolTip).slideUp(500);
    }

    function showToolTip(toolTip, that) {
        var pos = $(that).position();
        $(toolTip).css({
            top: pos.top + 28 + "px",
            left: pos.left - 50 + "px"
        }).slideDown(500);
    }

    $('img[id^="showDetails_"]').click(function (evt) {
        var detailsContainerToShow = evt.target.id.replace("showDetails_", "DetailsContainer_");
        $('#' + detailsContainerToShow).slideDown(1000);
    });

    $('img[id^="closeProject_"]').click(function (evt) {
        var projectId = evt.target.id.replace("closeProject_", "");
        $.ajax({
            type: 'POST',
            url: "/TaskBoard/CloseProject",
            data: { projectId: projectId },
            success: function (result) {
                $('#projectCreationOutcome').text = result;
            }
        });
    });
});

function getProject() {
    var name = $("#projectName").val();
    var description = $("#projectDescription").val();
    return {
        Name: name,
        Description: description
    };
}

function checkForNewProjectError() {
    if ($("#projectName").val() == "") {
        return "You must supply a project name.";
    }
    if ($("#projectDescription").val() == "") {
        return "You must supply a project description.";
    }
    return null;
}

function setProjectCreatedMessege(result) {
    var projectCreationOutcome = $("#projectCreationOutcome");
    projectCreationOutcome.removeClass("message-error");
    projectCreationOutcome.addClass("message-success");
    projectCreationOutcome.text(result.Name + " has been created.");
}