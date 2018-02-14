<?xml version="1.0" encoding="UTF-8"?>
<tileset name="Objects" tilewidth="180" tileheight="178" tilecount="8" columns="0">
 <grid orientation="orthogonal" width="1" height="1"/>
 <tile id="0">
  <image width="100" height="100" source="Camp.png"/>
 </tile>
 <tile id="1">
  <image width="180" height="178" source="Sprite-Trees.png"/>
 </tile>
 <tile id="2">
  <image width="58" height="40" source="Sprite-Rocks_02.png"/>
 </tile>
 <tile id="3">
  <image width="106" height="53" source="Sprite-Rocks_01.png"/>
 </tile>
 <tile id="4">
  <properties>
   <property name="activity" value="gather"/>
   <property name="capacity" type="int" value="-1"/>
   <property name="capacity_max" type="int" value="300"/>
   <property name="capacity_min" type="int" value="100"/>
   <property name="extract_daily" value="2"/>
   <property name="regen_rate" value="regular"/>
   <property name="resource" value="food"/>
   <property name="resource_type" value="berries"/>
   <property name="sizex" type="int" value="1"/>
   <property name="sizey" type="int" value="1"/>
   <property name="success_rate" type="int" value="100"/>
   <property name="type" value="trigger"/>
  </properties>
  <image width="71" height="58" source="Sprite-Herbs.png"/>
 </tile>
 <tile id="5">
  <image width="80" height="100" source="Puma.png"/>
 </tile>
 <tile id="12">
  <properties>
   <property name="spawner" value="player"/>
  </properties>
  <image width="54" height="61" source="Pion_Character.png"/>
 </tile>
 <tile id="13">
  <properties>
   <property name="spawner_mob" value="cat"/>
  </properties>
  <image width="65" height="29" source="Pion_Animal.png"/>
 </tile>
</tileset>
