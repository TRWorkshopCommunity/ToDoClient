var tasksManager = function() {

    // appends a row to the tasks table.
    // @parentSelector: selector to append a row to.
    // @obj: task object to append.
    var appendRow = function(parentSelector, obj) {
        var tr = $("<tr data-toDoId='" + obj.ToDoId + "' data-id='" + obj.Id + "'></tr>");
        tr.append("<td><input type='checkbox' class='completed' " + (obj.IsCompleted ? "checked" : "") + "/></td>");
        tr.append("<td class='name' >" + obj.Name + "</td>");
        tr.append("<td><input type='button' class='delete-button' value='Delete' /></td>");
        $(parentSelector).append(tr);
    };

    // adds all tasks as rows (deletes all rows before).
    // @parentSelector: selector to append a row to.
    // @tasks: array of tasks to append.
    var displayTasks = function(parentSelector, tasks) {
        $(parentSelector).empty();
        $.each(tasks, function(i, item) {
            appendRow(parentSelector, item);
        });
    };

    // starts loading tasks from server.
    // @returns a promise.
    var loadTasks = function() {
        return $.getJSON("/api/todos");
    };

    // starts creating a task on the server.
    // @isCompleted: indicates if new task should be completed.
    // @name: name of new task.
    // @return a promise.
    var createTask = function(isCompleted, name) {
        return $.post("/api/todos",
        {
            IsCompleted: isCompleted,
            Name: name
        });
    };

    // starts updating a task on the server.
    // @id: id of the task to update.
    // @isCompleted: indicates if the task should be completed.
    // @name: name of the task.
    // @return a promise.
    var updateTask = function(id, toDoId, isCompleted, name) {
        return $.ajax(
        {
            url: "/api/todos",
            type: "PUT",
            contentType: 'application/json',
            data: JSON.stringify({
                Id: id,
                ToDoId: toDoId,
                IsCompleted: isCompleted,
                Name: name
            }),
        });
    };

    var successCallback = function(data, textStatus) {
        displayTasks("#tasks > tbody", data);
    }

    // starts deleting a task on the server.
    // @taskId: id of the task to delete.
    // @return a promise.
    var deleteTask = function(id, toDoId) {
        return $.ajax({
            url: "/api/todos",
            type: 'DELETE',
            contentType: 'application/json',
            data: JSON.stringify({
                Id: id,
                ToDoId: toDoId
            })
        });
    };

    // returns public interface of task manager.
    return {
        loadTasks: loadTasks,
        displayTasks: displayTasks,
        createTask: createTask,
        deleteTask: deleteTask,
        updateTask: updateTask
    };
}();


$(function () {
    // add new task button click handler
    $("#newCreate").click(function() {
        var isCompleted = $('#newCompleted')[0].checked;
        var name = $('#newName')[0].value;

        tasksManager.createTask(isCompleted, name)
            .then(tasksManager.loadTasks)
            .done(function(tasks) {
                tasksManager.displayTasks("#tasks > tbody", tasks);
            });
    });

    // bind update task checkbox click handler
    $("#tasks > tbody").on('change', '.completed', function () {
        var tr = $(this).parent().parent();
        var toDoId = tr.attr("data-toDoId");
        var id = tr.attr("data-id");
        var isCompleted = tr.find('.completed')[0].checked;
        var name = tr.find('.name').text();
        
        tasksManager.updateTask(id, toDoId, isCompleted, name)
            .then(tasksManager.loadTasks)
            .done(function (tasks) {
                tasksManager.displayTasks("#tasks > tbody", tasks);
            });
    });

    // bind delete button click for future rows
    $('#tasks > tbody').on('click', '.delete-button', function () {
        var tr = $(this).parent().parent();
        var toDoId = tr.attr("data-toDoId");
        var id = tr.attr("data-id");
        tasksManager.deleteTask(id, toDoId)
            .then(tasksManager.loadTasks)
            .done(function(tasks) {
                tasksManager.displayTasks("#tasks > tbody", tasks);
            });
    });

    // load all tasks on startup
    tasksManager.loadTasks()
        .done(function(tasks) {
            tasksManager.displayTasks("#tasks > tbody", tasks);
        });
});