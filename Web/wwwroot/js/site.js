// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const tasks = document.querySelector('#productTasks');

if (tasks !== null) {
    const statuses = window.state.taskStatuses;

    const rowTemplate = (rowIndex, id, description, statusId) => `
         <div class="input-group mb-3 productTask">
                            <input type="hidden" name="Tasks[${rowIndex}].Id" value="${id}">
                            <div class="input-group-prepend">
                                <select  class="form-control custom-select" name="Tasks[${rowIndex}].StatusId">
                                    ${statuses.map(status => `<option ${status.id === parseInt(statusId) ? 'selected' : ''} value="${status.id}">${status.name}</option>`)}
                                </select>
                            </div>

                            <input type="text" class="form-control" placeholder="Task name" name="Tasks[${rowIndex}].Name" value="${description}" />
                            <div class="input-group-append">
                                <button class="btn btn-outline-danger deleteProductTask" type="button">Delete</button>
                            </div>
                        </div>
    `;


    let taskId = 0;

    tasks.innerHTML += window.state.tasks.map(task => rowTemplate(taskId++, task.id, task.name, task.status.id)).join('');
    const createBtn = document.querySelector('#createTask');
    const createDescription = document.querySelector('#taskDescription');

    createBtn.addEventListener('click', (e) => {
        const description = document.querySelector('#taskDescription');
        const status = document.querySelector('#taskStatus');

        tasks.innerHTML += rowTemplate(taskId++, -1, description.value, status.value);
        createDescription.value = "";
    });

    tasks.addEventListener('click', evt => {
        if (evt.target.classList.contains('deleteProductTask')) {
            evt.target.closest('.productTask').remove();
        }
    })

}
