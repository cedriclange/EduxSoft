// Copyright © 2017-2019 SOFTINUX. All rights reserved.
// Licensed under the MIT License, Version 2.0. See LICENSE file in the project root for license information.

///<reference path = '../../SoftinuxBase.Barebone/node_modules/jquery/dist/jquery.js' />
///<reference path = '../../SoftinuxBase.Barebone/node_modules/toastr/toastr.js' />

/* Anonymous function to avoid polluting global namespace */
$(function () {
    'use strict';

    window.toastr.options.positionClass = 'toast-top-right';

    browseForAvatar();

    /* User interactions that trigger UI changes but no ajax call */

    // Manage click on buttons
    $('button').click( function(event_) {
        const id = $(this).attr('id');
        switch(id){
            case 'save_profile_btn':
                edit_state('profile_form_fieldset', 'save_profile_btn', event_);
                break;
            case 'cancel_save_profile_btn':
                cancel_edit_state('profile_form', 'profile_form_fieldset', 'save_profile_btn', 'Edit', event_);
                break;
            case 'change_pwd-btn':
                edit_state('pwd_form_fliedset', 'change_pwd-btn', event_);
                break;
            case 'cancel_change_pwd-btn':
                cancel_edit_state('pwd_form', 'pwd_form_fliedset', 'change_pwd-btn', 'Change', event_);
                break;
            case 'add-role-btn':
                $('#edit-role-area').addClass('hidden');
                $('#add-role-area').is(':hidden') ? $('#add-role-area').removeClass('hidden') : $('#add-role-area').addClass('hidden');
                break;
            case 'edit-role-btn':
                $('#add-role-area').addClass('hidden');
                $('#edit-role-area').is(':hidden') ? $('#edit-role-area').removeClass('hidden') : $('#edit-role-area').addClass('hidden');
                break;
            case 'cancel-add-role-btn':
                $('#edit-role-area').addClass('hidden');
                $('#add-role-area').addClass('hidden');
                break;
            case 'cancel-edit-role-btn':
                $('#edit-role-area').addClass('hidden');
                $('#add-role-area').addClass('hidden');
                break;
            // Add selected/unselected extensions management
            case 'addRoleBtnRight':
                btnChevronMoveExtension(event_);
                break;
            case 'addRoleBtnAllRight':
                btnChevronMoveExtension(event_);
                break;
            case 'addRoleBtnLeft':
                btnChevronMoveExtension(event_);
                break;
            case 'addRoleBtnAllLeft':
                btnChevronMoveExtension(event_);
                break;
            // Edit selected/unselected extensions management
            case 'editRoleBtnRight':
                btnChevronMoveExtension(event_, 'to-right');
                break;
            case 'editRoleBtnAllRight':
                btnChevronMoveExtension(event_, 'to-right');
                break;
            case 'editRoleBtnLeft':
                btnChevronMoveExtension(event_, 'to-left');
                break;
            case 'editRoleBtnAllLeft':
                btnChevronMoveExtension(event_, 'to-left');
                break;
            default:
                break;
        }
    });

    // permissions administration: collapsing
    $('#collapse').on('click', function () {
        const state = $(this).data('state');
        if (state === 'closed') {
            $(this).data('state', 'open');
            // TODO change icon to open double chevron

            // open all the collapsed children
            $('.extension-row.collapsed').each(function () {
                $(this).removeClass('collapsed');
                $(this).data('aria-expanded', 'true');
                $('.row.collapse').each(function () {
                    $(this).addClass('in');
                });
            });

        } else {
            $(this).data('state', 'closed');
            // TODO change icon to closed double chevron

            // collapse all the children
            $('.extension-row').each(function () {
                if (!$(this).hasClass('collapsed')) {
                    $(this).addClass('collapsed');
                    $(this).data('aria-expanded', 'false');
                    $('.row.collapse').each(function () {
                        $(this).removeClass('in');
                    });
                }
            });
        }
    });

    // permission dropdown
    $('#acl-sel li').click(function (event_) {
        const target = $(event_.currentTarget);
        target.closest('.bs-dropdown-to-select-acl-group')
            .find('[data-bind="bs-drp-sel-acl-label"]').text($(this).text());
        $('input[name="acl-selected_value"]').val($(this).attr('data-value'));
    });

    // Keyup, change, paste
    $('#profile_form :input').bind('keyup change paste', function () {
        input_changed('save_profile_btn');
    });

    $('#pwd_form :input').bind('keyup change paste', function () {
        input_changed('change_pwd-btn');
    });

    // Change
    $('#inputAvatar').change(function () {
        $('#file_path').val($(this).val());
    });

    $('#role_name_input').change(function () {
        input_form_group_validator('#role_name_input');
    });

    // Focusout
    $('#role_name_input').focusout(function () {
        input_form_group_validator('#role_name_input');
    });

    /* User interactions that trigger ajax calls */

    // save new role with its extensions and permission
    $('#save-add-role-btn').click(function () {
        const roleNameInputElt = $('#role_name_input');
        if (!roleNameInputElt.val()) {
            window.toastr.warning('No role name given.', 'Role not saved!');
            input_form_group_validator('#role_name_input');
            return;
        }
        var selectedExtensions = [];
        $('#addRoleRightExtensionsList > option').each(function () {
            selectedExtensions.push(this.value);
        });
        const postData = {
            RoleName: roleNameInputElt.val(),
            Extensions: selectedExtensions,
            PermissionValue: $('#newRolePermission').val()
        };

        $.ajax('/administration/save-new-role', { method: 'POST', data: postData })
            .done(function (data_) {
                window.toastr.success(data_, 'New role created');
                input_form_group_set_error('#role_name_input', null);
                location.reload();
            })
            .fail(function (jqXhr_, testStatus_) {
                const errMsg = jqXhr_.responseText ? jqXhr_.responseText : testStatus_;
                input_form_group_set_error('#role_name_input', errMsg);
            });
    });

    $('#save-edit-role-btn').click(function () {
        if (!$('#edit_role_name_input').val()) {
            window.toastr.warning('No new role name given.', 'Role not updated!');
            input_form_group_validator('#edit_role_name_input');
            return;
        }

        saveEditRole();
    });

});

/* Function declared in global namespace
so that they can be called by inline event handlers (onclick='...')
or by functions declared in anonymous function above */

/**
 * browse for avatar
 */
function browseForAvatar() {
    $('#file_browser').click(function (event_) {
        event_.preventDefault();
        $('#inputAvatar').click();
    });
}

/*----------------------------------------------------------------*/
/*------------------------ events handler ------------------------*/
/*----------------------------------------------------------------*/

/**
 * Copy selected item(s) from left listbox to right listbox
 *
 * @param {object} event_ - dom event
 * @param {string} transform_ - how to transform the event target :
 * - null: just clone the target
 * - "to-left": transform the html fragment to a html span element
 * - "to-right": transform a html span element to an html fragment
 */
function btnChevronMoveExtension(event_, transform_) {

    var _target = event_.target;

    if (_target.tagName === 'I')
        _target = _target.parentNode;

    const bulk = $(_target).is('[data-bulk-move]');

    // if transform_ is defined, the selected list items are div elements, else select's options

    let rootElt = $(`#${$(_target).attr('data-fromlist')}`);
    const selectedElts = transform_
    ? rootElt.find(bulk ? ' div.row' : ' div.row.active').toArray()
    : rootElt.find(bulk ? ' option' : ' option:selected').toArray();

    // console.log(selectedElts);

    if (selectedElts.length === 0) {
        const emptyExtensionList = bulk ? 'You must have at least one extension in the list' : 'You must select at least one extension in the list';
        const emptyExtensionListTitle = 'No extension to move';
        window.toastr.warning(emptyExtensionList, emptyExtensionListTitle);
        event_.preventDefault();
        return;
    }

    let newElts = [];
    switch (transform_) {
        case 'to-left':
            newElts = selectedElts.map(createMovedElementLeft);
            break;
        case 'to-right':
            newElts = selectedElts.map(createMovedElementRight);
            break;
        default:
            newElts = selectedElts.map(function(elt) { return elt.outerHTML});
            break;
    }
    for(let newElt of newElts) {
        // append the html element or string
        $(`#${$(_target).attr('data-tolist')}`).append(newElt);
    }
    $(selectedElts).remove();
    event_.preventDefault();

    // Now reapply the clicks handlers
    // left and right lists rows
    $('#editRoleLeftExtensionsList>div').off('click');
    $('#editRoleLeftExtensionsList>div').click(function(event_) {
        row_clicked(event_);
    });

    $('#editRoleRightExtensionsList>div').off('click');
    $('#editRoleRightExtensionsList>div.row').click(function(event_) {
        row_clicked(event_);
    });
}

/**
 * Create a html fragment containing mostly a span element, from a html fragment containing mostly a span element.
 * @param {HtmlElement} target_ - html div element
 * @return {string} html div
 */
function createMovedElementLeft(target_) {
    return `<div class="row modified">
                <div class="col-md-12">${$(target_).find('span').get(0).outerHTML}</div>
            </div>`;
}

/**
 * Create a html fragment containing mostly a span and a select elements, from a html fragment contaning mostly a span and a select elements.
 * @param {HtmlElement} target_ - html div element
 * @return {string} html div
 */
function createMovedElementRight(target_) {
    let extension = $(target_).find('span').attr("name");
    return `<div class="row modified">
                <div class="col-md-6">
                    <span name="${extension}">${extension}</span>
                </div>
                <div class="col-md-6">
                    <select>
                        <option value="None">None</option>
                        <option value="Read" selected>Read</option>
                        <option value="Write">Write</option>
                        <option value="Admin">Admin</option>
                    </select>
                </div>
            </div>`;
}

/**
 *
 * @param {any} fieldsetid_ - fieldsetid_
 * @param {any} editbtnid_ - editbtnid_
 * @param {any} event_ - event_
 */
function edit_state(fieldsetid_, editbtnid_, event_) {
    event_.preventDefault();
    $(`#cancel_${editbtnid_}`).removeClass('hidden');
    $(`button#${editbtnid_}`).addClass('hidden');
    if (editbtnid_ === 'save_profile_btn') {
        $('#file_browser').addClass('btn-primary');
        $('#file_browser').removeClass('btn-default');
    }
    if ($(`#${fieldsetid_}`).prop('disabled')) {
        $(`#${fieldsetid_}`).removeAttr('disabled');
        $(`#${editbtnid_}`).prop('disabled', true);
    }
}

/**
 *
 * @param {any} formid_ - formid_
 * @param {any} fieldsetid_ - fieldsetid_
 * @param {any} editbtnid_ - editbtnid_
 * @param {any} editbtntxt_ - editbtntxt_
 * @param {any} event_ - event_
 */
function cancel_edit_state(formid_, fieldsetid_, editbtnid_, editbtntxt_, event_) {
    event_.preventDefault();
    $(`#${fieldsetid_}`).prop('disabled', true);
    $(`#${editbtnid_}`).prop('disabled', false);
    $(`button#${editbtnid_}`).text(editbtntxt_);
    $(`#cancel_${editbtnid_}`).addClass('hidden');
    $(`button#${editbtnid_}`).removeClass('hidden');
    $('#file_browser').removeClass('btn-primary');
    $(`#${editbtnid_}`).removeClass('btn-success').addClass('btn-primary');
    $(`#${formid_}`)[0].reset();
}

/**
 *
 * @param {any} editbtnid_ - editbtnid_
 */
function input_changed(editbtnid_) {
    $(`#${editbtnid_}`).prop('disabled', false);
    $(`#${editbtnid_}`).removeClass('btn-primary').addClass('btn-success');
    $(`button#${editbtnid_}`).text('Save');
    $(`#cancel_${editbtnid_}`).removeClass('hidden');
    $(`button#${editbtnid_}`).removeClass('hidden');
}

function row_clicked(event_) {
    const target = $(event_.currentTarget);
    if(target.hasClass('active')) {
        target.removeClass('active');
    } else {
        target.addClass('active');
    }
}

/**
 * display modal to choose what to remove about role
 */
function removeRoleLink() {
    console.log('Role name: ', $(event.target).parent().data('roleId'));
    const splitted = $(event.target).parent().data().roleId.split('_');
    console.log('Extension name: ', splitted[0], ' Role name: ', splitted[1]);

    $('#moduleName').text(splitted[0]);
    $('#roleName').text(splitted[1]);

    $('#myModal').modal('show');
}

/**
 * Set error style to input when its value is empty.
 * @param {string} el_ - jQuery selector string.
 */
function input_form_group_validator(el_) {
    if (!$(el_).is('input')) {
        return;
    }

    if ($(el_).val()) {
        $(el_).closest('.form-group').removeClass('has-error').removeClass('has-feedback');
    } else {
        $(el_).closest('.form-group').addClass('has-error').addClass('has-feedback');
    }
}

/**
 * Set error style to an input and error message below.
 * @param {string} element_ - jQuery selector string.
 * @param {string} errMsg_ - error message if any error, else null to remove error style and message.
 */
function input_form_group_set_error(element_, errMsg_) {
    if (!$(element_).is('input')) {
        return;
    }
    const formGroupEl = $(element_).closest('.form-group');
    if (!errMsg_) {
        formGroupEl.removeClass('has-error').removeClass('has-feedback');
        formGroupEl.find('span.help-block').html('');
    } else {
        formGroupEl.addClass('has-error').addClass('has-feedback');
        formGroupEl.find('span.help-block').html(errMsg_);
    }
}

/*-------------------------------------------------------------------------------------*/
/*------------------------ Functions that trigger an ajax call ------------------------*/
/*-------------------------------------------------------------------------------------*/

/**
 * Update the UI with selected role information. Ajax GET.
 * @param {any} roleId_ - roleId
 */
function passSelectedRoleOnEdition(roleId_) {
    $('#edit-role-group').removeClass('has-error');
    $.ajax('/administration/read-role', { data: { 'roleId_': roleId_ } }).done(function (data_) {
        const role = data_.value.role;
        for (let key in role) {
            $(`#edit_role_${key}`).val(role[key]);
        }

        // Role name
        $('#edit_role_name_input').val(role.name);

        // Role ID
        $('#editRoleId').val(roleId_);

        // Available extensions (left list)
        let leftListElt = $('#editRoleLeftExtensionsList');
        // Clear
        leftListElt.html("");
         // Fill
        data_.value.availableExtensions.forEach(function (extension_) {
            leftListElt.append(`<div class="row"><div class="col-md-12"><span name="${extension_}">${extension_}</span></div></div>`);
        });

        // Selected extensions (right list)
        let rightListElt = $('#editRoleRightExtensionsList');
        // Clear
        rightListElt.html("");
        // Fill
        data_.value.selectedExtensions.forEach(function (extension_) {
            rightListElt.append(`<div class="row">
                            <div class="col-md-6">
                                <span name="${extension_.extensionName}">${extension_.extensionName}</span>
                            </div>
                            <div class="col-md-6">
                                <select>
                                    <option value="None" ${extension_.permissionName === 'None' ? "selected" : ""}>None</option>
                                    <option value="Read" ${extension_.permissionName === 'Read' ? "selected" : ""}>Read</option>
                                    <option value="Write" ${extension_.permissionName === 'Write' ? "selected" : ""}>Write</option>
                                    <option value="Admin" ${extension_.permissionName === 'Admin' ? "selected" : ""}>Admin</option>
                                </select>
                            </div>
                        </div>`);
        });

        // Now add the clicks handlers
        // left and right lists rows
        $('#editRoleLeftExtensionsList>div').click(function(event_) {
            row_clicked(event_);
        });

        $('#editRoleRightExtensionsList>div.row').click(function(event_) {
            row_clicked(event_);
        });
    });
}

/**
 * Click in permission checkbox. Calls savePermission().
 */
function checkClick() {
    const splitted = $(event.target)[0].id.split('_');
    const base = splitted[0] + '_' + splitted[1];
    var _activeCheckedPermissions = 'NEVER';

    if (event.target.checked) {
        // when checking, impacted checkboxes become checked and disabled
        switch (splitted[2]) {
            case 'ADMIN':
                $(`#${base}_WRITE, #${base}_READ`).prop('checked', true).prop('disabled', true);
                break;
            case 'WRITE':
                $(`#${base}_READ`).prop('checked', true).prop('disabled', true);
                break;
        }
        savePermission(splitted[0], splitted[1], splitted[2]);
    }
    else {
        // when unchecking, first next checkbox becomes enabled
        switch (splitted[2]) {
            case 'ADMIN':
                $(`#${base}_WRITE`).prop('disabled', false);
                _activeCheckedPermissions = 'WRITE';
                break;
            case 'WRITE':
                $(`#${base}_READ`).prop('disabled', false);
                _activeCheckedPermissions = 'READ';
                break;
        }
        savePermission(splitted[0], splitted[1], _activeCheckedPermissions);
    }
}

/**
 * Ajax call to update data: role-extension-permission link update. Ajax GET (TODO change to POST).
 * @param {any} extension_ - extension
 * @param {any} roleName_ - role name
 * @param {any} permission_ - permission (enum value)
 */
function savePermission(extension_, roleName_, permission_) {
    const params = {
        'roleName_': roleName_,
        'permissionValue_': permission_,
        'extension_': extension_
    };
    $.ajax('/administration/update-role-permission', { data: params });
}

/**
 * Ajax call to update data: role with its related data update. Ajax POST.
 */
function saveEditRole() {

    var grants = [];
    $('#editRoleRightExtensionsList>div.row').each(function (index, elt) {
       grants.push({Extension: $(elt).find('span').attr('name'), PermissionValue: $(elt).find('select').val()})
    });
    const postData = {
       RoleId: $('#editRoleId').val(),
       RoleName: $('#edit_role_name_input').val(),
       Grants: grants
    };

    $.ajax('/administration/update-role',
       {
           method: 'POST',
           data: postData
       })
       .done(function (data_) {
           window.toastr.success(data_, 'Changes saved');
           location.reload();
       });
}

function deleteRole(role_) {
    const postData = {
        'roleName_': role_
    };

    $.ajax('/administration/delete-role',
        {
            method: 'POST',
            data: postData
        })
        .done(function (data_) {
            window.toastr.success(data_, 'Role deleted');
            console.log(data_);
        });
}