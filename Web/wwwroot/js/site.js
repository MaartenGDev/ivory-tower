// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const tasksElem = document.querySelector('#productTasks');

if (tasksElem !== null) {
    const statuses = window.state.taskStatuses;

    const rowTemplate = (rowIndex, id, description, statusId) => `
         <div class="input-group mb-3 productTask" data-task-id="${id}">
                            <input type="hidden" name="Tasks[${rowIndex}].Id" value="${id}">
                            <div class="input-group-prepend">
                                <select  class="form-control custom-select" name="Tasks[${rowIndex}].Status.Id">
                                    ${statuses.map(status => `<option ${status.id === parseInt(statusId) ? 'selected' : ''} value="${status.id}">${status.name}</option>`)}
                                </select>
                            </div>

                            <input type="text" class="form-control" placeholder="Task name" name="Tasks[${rowIndex}].Name" value="${description}" />
                            <div class="input-group-append">
                                <button class="btn btn-outline-danger deleteProductTask" type="button">Delete</button>
                            </div>
                        </div>
    `;



    const createBtn = document.querySelector('#createTask');
    const createDescription = document.querySelector('#taskDescription');

    createBtn.addEventListener('click', (e) => {
        const description = document.querySelector('#taskDescription');
        const status = document.querySelector('#taskStatus');

        window.state.tasks.push({id: -window.state.tasks.length, name: description.value, status: {id: parseInt(status.value)}});
        renderTasksList(window.state.tasks);
        createDescription.value = "";
    });
    
    const renderTasksList = (tasks) => {
        let taskId = 0;
        tasksElem.innerHTML = tasks.map(task => rowTemplate(taskId++, task.id, task.name, task.status.id)).join('');
    };
    

    tasksElem.addEventListener('click', evt => {
        if (evt.target.classList.contains('deleteProductTask')) {
            const wrapper = evt.target.closest('.productTask');
            const taskId = parseInt(wrapper.dataset.taskId);
            wrapper.remove();

            window.state.tasks = window.state.tasks.filter(x => x.id !== taskId);
            
            renderTasksList(window.state.tasks);
        }
    });

    renderTasksList(window.state.tasks);
}
