$(document).ready(function() {

    $("#addNewProjectLink").click(function(evt) {

        evt.preventDefault();
        var projectError = checkForNewProjectError();
        if (projectError != null) {
            $("#projectCreationOutcome").text = projectError;
        } else {
            var project = getProject();
            alert(project.Name + " " + project.Description);
            $.post("/TaskBoard/CreateProject", project, function(result) {
                $('#projectCreationOutcome').text = result.name + " has been created.";
            });
        }
    });

    $('.projectAdminLinks a').hover(
        function() {
            showToolTip('#editProjectToolTip', this);
        },
        function() {
            hideToolTip('#editProjectToolTip', this);
        });

    $('img[id^="showDetails_"]').hover(
        function() {
            showToolTip('#ShowDescriptionToolTip', this);
        },
        function() {
            hideToolTip('#ShowDescriptionToolTip', this);
        });

    $('img[id^="closeProject_"]').hover(
        function() {
            showToolTip('#closeToolTip', this);
        },
        function() {
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

    $('img[id^="showDetails_"]').click(function(evt) {
        var detailsContainerToShow = evt.target.id.replace("showDetails_", "DetailsContainer_");
        $('#' + detailsContainerToShow).slideDown(1000);
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

});