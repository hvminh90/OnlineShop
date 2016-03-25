/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    //config.syntaxhighlight_lang = 'csharp';
    //config.syntaxhighlight_hideControls = true;
    //config.language = 'vi';
    //config.filebrowserBrowseUrl = '/assets/admin/js/plugins/ckfinder/ckfinder.html';
    //config.filebrowserImageBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Images';
    //config.filebrowserFlashBrowseUrl = '/assets/admin/js/plugins/ckfinder.html?Type=Flash';
    //config.filebrowserUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    //config.filebrowserImageUploadUrl = '/Data';
    //config.filebrowserFlashUploadUrl = '/assets/admin/js/plugins/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    //CKFinder.setupCKEditor(null, '/assets/admin/js/plugins/ckfinder/');

    config.language= 'vi',
     
    config.extraPlugins = "imagebrowser,insertpre,sourcedialog,font",
    config.height = 300;
    //imageBrowser_listUrl: "ImageBrowser.aspx",
    //filebrowserBrowseUrl:  "LinkBrowser.aspx",
    //filebrowserImageUploadUrl:  "ImageBrowser.aspx",
    //filebrowserUploadUrl: "LinkBrowser.aspx",

    config.filebrowserImageBrowseUrl = CKEDITOR.basePath + "ImageBrowser.aspx",
    config.filebrowserImageWindowWidth = 780,
    config.filebrowserImageWindowHeight = 580,
    config.filebrowserBrowseUrl= CKEDITOR.basePath + "LinkBrowser.aspx",
    config.filebrowserWindowWidth = 500,
    config.filebrowserWindowHeight= 650,

    config.toolbarLocation = 'top',
    config.toolbar = 'full',
    config.toolbar_full = [
        {
            name: 'basicstyles',
            items: ['Bold', 'Italic', 'Strike', 'Underline', 'Source']
        },
        { name: 'paragraph', items: ['BulletedList', 'NumberedList', 'Blockquote'] },
        { name: 'editing', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
        { name: 'tools', items: ['SpellChecker', 'Maximize'] },
        { name: 'clipboard', items: ['Undo', 'Redo'] },
        { name: 'styles', items: ['Format', 'FontSize', 'TextColor', 'PasteText', 'PasteFromWord', 'RemoveFormat', 'Font'] },
        { name: 'insert', items: ['Image', 'Table', 'SpecialChar'] }, '/',
    ]

};
