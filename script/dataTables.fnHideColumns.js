/* Hides dataTable columns by column headers (allows multiple)
 * Usage: jQuery('#my-results').dataTableHideCol("Status", "Comments");
 * Author: Richard Lee
 * Version 1.0
 * Requires jQuery dataTables
 */
(function($) {
	$.fn.removeColumnByName = function(){
		// check everything is ok
		if($.dataTable == "undefined" || !$(this).length || !arguments.length){
			return false;
		}
		var colNames = arguments;
		//console.log(cols);
		return this.each(function(){
			/* Get the DataTables object again - this is not a recreation, just a get of the object */
			var $this = $(this);
			var $dTable = $this.dataTable();
			//var dTable = $this.dataTable();
			for(var i = 0; i < colNames.length; i++){
				var colIndex = $this.getColumnNumber(colNames[i]);
				if(colIndex!=-1){ // check column exists
					$dTable.fnSetColumnVis( colIndex, false, true );
				}
			}
		});
	};
	/* 
	fnSetColumnVis() screws up DOM indexes so we cant use jQuery.index() etc so grab cols from dataTable object
	(thanks to http://datatables.net/forums/discussion/6777/filtering-multiple-columns-problem-when-hiding-columns/p1)
	*/
	$.fn.getColumnNumber = function(name) {
		name = jQuery.trim(name).toLowerCase();  // case insensitive match, cast all to lowercase
		var $dTable = $(this).dataTable(); 
		var aoColumns = $dTable.fnSettings().aoColumns;
		var numcols = aoColumns.length;
		for (var i=0; i<numcols; i++) {
			// check for sname, else use text with th
			if(jQuery.trim(aoColumns[i].sName).toLowerCase() == name || jQuery.trim(aoColumns[i].nTh.innerText).toLowerCase() == name){
				return i;
			}
		}
		return -1;
	};

	
})(jQuery);