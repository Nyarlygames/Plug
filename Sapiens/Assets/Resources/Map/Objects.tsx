<?xml version="1.0" encoding="UTF-8"?>
<tileset name="Objects" tilewidth="500" tileheight="500" tilecount="16" columns="0">
 <grid orientation="orthogonal" width="1" height="1"/>
 <tile id="0">
  <image width="100" height="100" source="Tribe_Camp.png"/>
 </tile>
 <tile id="1">
  <properties>
   <property name="collider" type="bool" value="true"/>
  </properties>
  <image width="180" height="178" source="Texture_Trees.png"/>
 </tile>
 <tile id="2">
  <image width="106" height="53" source="Texture_Rock1.png"/>
 </tile>
 <tile id="3">
  <image width="58" height="40" source="Texture_Rock2.png"/>
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
  <image width="71" height="58" source="Texture_Herbs.png"/>
 </tile>
 <tile id="5">
  <properties>
   <property name="activity" value="hunt"/>
   <property name="capacity" type="int" value="-1"/>
   <property name="capacity_max" type="int" value="200"/>
   <property name="capacity_min" type="int" value="50"/>
   <property name="extract_daily" value="4"/>
   <property name="regen_rate" value="regular"/>
   <property name="resource" value="food"/>
   <property name="resource_type" value="meat"/>
   <property name="sizex" type="int" value="1"/>
   <property name="sizey" type="int" value="1"/>
   <property name="success_rate" type="int" value="75"/>
   <property name="type" value="trigger"/>
  </properties>
  <image width="80" height="100" source="Hunt_Puma.png"/>
 </tile>
 <tile id="12">
  <properties>
   <property name="spawner" value="player"/>
  </properties>
  <image width="54" height="61" source="Placeholder_Spawner.png"/>
 </tile>
 <tile id="13">
  <properties>
   <property name="spawner_mob" value="cat"/>
  </properties>
  <image width="65" height="29" source="Pion_Animal.png"/>
 </tile>
 <tile id="14">
  <properties>
   <property name="activity" value="hunt"/>
   <property name="capacity" type="int" value="-1"/>
   <property name="capacity_max" type="int" value="200"/>
   <property name="capacity_min" type="int" value="50"/>
   <property name="extract_daily" value="4"/>
   <property name="regen_rate" value="regular"/>
   <property name="resource" value="food"/>
   <property name="resource_type" value="swarm"/>
   <property name="sizex" type="int" value="1"/>
   <property name="sizey" type="int" value="1"/>
   <property name="success_rate" type="int" value="75"/>
   <property name="type" value="trigger"/>
  </properties>
  <image width="100" height="100" source="FOOD/Food_Meat.png"/>
 </tile>
 <tile id="15">
  <image width="100" height="100" source="FOOD/Food_Mushroom.png"/>
 </tile>
 <tile id="16">
  <image width="100" height="100" source="FOOD/Food_Insect.png"/>
 </tile>
 <tile id="17">
  <image width="100" height="100" source="FOOD/Food_Clam.png"/>
 </tile>
 <tile id="18">
  <image width="100" height="100" source="FOOD/Food_Clam.png"/>
 </tile>
 <tile id="19">
  <properties>
   <property name="activity" value="fish"/>
   <property name="capacity" type="int" value="-1"/>
   <property name="capacity_max" type="int" value="500"/>
   <property name="capacity_min" type="int" value="100"/>
   <property name="extract_daily" value="4"/>
   <property name="regen_rate" value="regular"/>
   <property name="resource" value="food"/>
   <property name="resource_type" value="smallfish"/>
   <property name="sizex" type="int" value="1"/>
   <property name="sizey" type="int" value="1"/>
   <property name="success_rate" type="int" value="70"/>
   <property name="type" value="trigger"/>
  </properties>
  <image width="100" height="100" source="FOOD/Food_Fish.png"/>
 </tile>
 <tile id="20">
  <properties>
   <property name="activity" value="source"/>
   <property name="capacity" type="int" value="-1"/>
   <property name="capacity_max" type="int" value="-1"/>
   <property name="capacity_min" type="int" value="-1"/>
   <property name="extract_daily" value="10"/>
   <property name="regen_rate" value="regular"/>
   <property name="resource" value="water"/>
   <property name="resource_type" value="river"/>
   <property name="sizex" type="int" value="1"/>
   <property name="sizey" type="int" value="1"/>
   <property name="success_rate" type="int" value="100"/>
   <property name="type" value="trigger"/>
  </properties>
  <image width="31" height="27" source="ICONS/Icon_Source.png"/>
 </tile>
 <tile id="21">
  <properties>
   <property name="chunkfile" value=""/>
   <property name="loadChunk" type="int" value="0"/>
  </properties>
  <image width="500" height="500" source="Chunk_Loader.png"/>
 </tile>
</tileset>
