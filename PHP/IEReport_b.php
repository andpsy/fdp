<?php
require "config.php";

$link = mysqli_connect ($dbhost, $dbuser, $dbpass, $dbname);
if (!$link) {
    die('Could not connect: ' . mysql_error());
}
if ($result = mysqli_query($link, "call OWNERSsp_IE_DETAILS_REPORT(199, NULL,NULL,NULL,NULL,63,'2009-01-01',CURDATE())"))
{
?>
   <table border="0" cellpadding="0" cellspacing="0" class="leftmenu">
      <tr>
         <td valign="top" class="leftmenutop">
        <?php
		echo "< table >";
		$fieldarray = array("id","title","description");
		maketable("SELECT * FROM bw_news", $fieldarray);
		echo "< /table >";
        /*
        $div_count = 0;
        while( $row = mysqli_fetch_array($result) )
        {
                $mdivId = 'submenu_'.$div_count;
                $mcategorie_id = $row['ID'];
                $name = $row['NAME'];
                $cnt0 = $row['CNT']==null?0:$row['CNT'];
                $link2 = mysqli_connect ($dbhost, $dbuser, $dbpass, $dbname);
                if ($subresult = mysqli_query($link2, "call CATEGORIES_MATERIALSsp_GetById('$mcategorie_id')"))
                {
                  if(mysqli_num_rows($subresult)>0)
                  {
                     echo "<li class='liniv1'><h1><a href='#null' alt='".$name."' title='".$name."' onclick='javascript:switchDiv(\"submenu_\",$div_count)'>$name ($cnt0)</a></h1></li>";
                     echo ("<div id='$mdivId' style='display:".($mdivId == $_GET['divId']?'block':'none').";'>");
                     while( $subrow = mysqli_fetch_array($subresult) )
                     {
                        $mmaterial_name = $subrow['material_name'];
                        $mmaterial_id = $subrow['material_id'];
                        $cnt1 = $subrow['CNT']==null?0:$subrow['CNT'];
                        if($mmaterial_name != null && $mmaterial_id != null)
                        {
                           //echo "<li class='liniv2'><h2><a href='categorie.php?divId=$mdivId&categorie_id=$mcategorie_id&material_id=$mmaterial_id&manufacturer_id=$manufacturer_id'>$mmaterial_name ($cnt1)</a></h2></li>";
                           echo "<li class='liniv2'><h2><a href='$mdivId-$mcategorie_id-$mmaterial_id-$manufacturer_id.html' alt='".$name." - ".$mmaterial_name."' title='".$name." - ".$mmaterial_name."'>$mmaterial_name ($cnt1)</a></h2></li>";
                        }
                     }
                     echo "</div>";
                     $div_count++;
                  }
                  else
                  {
                     //echo "<li class='liniv1'><h1><a href='categorie.php?categorie_id=$mcategorie_id'>$name ($cnt0)</a></h1></li>";
                     echo "<li class='liniv1'><h1><a href='null-$mcategorie_id-null-null.html' alt='".$name."' title='".$name."'>$name ($cnt0)</a></h1></li>";
                  }
                  mysqli_free_result($subresult);
                }
                mysqli_close($link2);
        }
        mysqli_free_result($result);
        */
}
mysqli_close($link);
?>
         </td>
      </tr>
   </table>

