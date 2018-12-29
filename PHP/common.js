function switchDiv(divId, imgId, divId2)
{
	var _div = document.getElementById(divId);
	var _img = document.getElementById(imgId);
	var _div_report_content = document.getElementById(divId2);
	if(_div != null && _div != 'undefined')
	{
		_div.style.display = _div.style.display=='block'?'none':'block';
		_img.src = _div.style.display=='block'?'08_descending.gif':'08_ascending.gif';
		switch(divId2)
		{
			case "report_content":
			_div_report_content.style.top = _div.style.display=='block'?'260px':'60px';
			break;
		}
	}
}
