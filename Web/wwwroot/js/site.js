// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const tasks = document.querySelector('#inputTasks');

if (tasks !== null) {
    const statuses = window.state.taskStatuses;

    const rowTemplate = (description) => `
         <div class="input-group mb-3 productTask">
                            <div class="input-group-prepend">
                                <select name="CategoryId" class="form-control custom-select">
                                    ${statuses.map(status => `<option value="${status.id}">${status.name}</option>`)}
                                </select>
                            </div>

                            <input type="text" class="form-control" placeholder="Task description" value="${description}">
                            <div class="input-group-append">
                                <button class="btn btn-outline-danger deleteProductTask" type="button">Delete</button>
                            </div>
                        </div>
    `;

    const createBtn = document.querySelector('#createTask');

    tasks.addEventListener('click', evt => {
        if (evt.target.id === createBtn.id) {
            tasks.innerHTML = rowTemplate(document.querySelector('#taskDescription').value) + tasks.innerHTML;
        }

        if (evt.target.classList.contains('deleteProductTask')) {
            evt.target.closest('.productTask').remove();
        }
    })

}
