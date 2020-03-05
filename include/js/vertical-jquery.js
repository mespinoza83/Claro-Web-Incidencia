jQuery(document).ready(function() {
        // Slide
        // Expand only the active menu, which is determined by the class name
        // Toggle the selected menu's class and expand or collapse the menu
	jQuery("#navbar > ul > li > a[class=parent]").find("+ ul").slideToggle("medium");
        // Toggle the selected menu's class and expand or collapse the menu
        jQuery("#navbar > ul > li > a").click(function() {
            jQuery(this).toggleClass("parent").toggleClass("collapsed").find("+ ul").slideToggle("medium");
        });
});