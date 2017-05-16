//Maya ASCII 2015 scene
//Name: kart.ma
//Last modified: Mon, May 15, 2017 05:44:48 PM
//Codeset: UTF-8
requires maya "2015";
currentUnit -l centimeter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2015";
fileInfo "version" "2015";
fileInfo "cutIdentifier" "201405190330-916664";
fileInfo "osv" "Mac OS X 10.9.6";
fileInfo "license" "student";
createNode transform -s -n "persp";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -40.360773173771236 31.287301306190777 16.113994673824525 ;
	setAttr ".r" -type "double3" -48.3383527304279 23.400000000330795 0 ;
createNode camera -s -n "perspShape" -p "persp";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999986;
	setAttr ".coi" 26.413812137957613;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".tp" -type "double3" -47.333921427594632 11.553983162028571 -1.7881393432617188e-07 ;
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	setAttr ".v" no;
	setAttr ".t" -type "double3" -47.333921308385342 113.16045010218157 -2.3841855600892359e-07 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 27.052363066428654;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	setAttr ".t" -type "double3" -49.495384187319523 -1.2926719361403052 114.0610501700633 ;
createNode camera -s -n "frontShape" -p "front";
	setAttr -k off ".v";
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 58.516674789531123;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	setAttr ".t" -type "double3" 100.11304277960193 -1.2926719361403052 -1.4938034109113998 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	setAttr -k off ".v";
	setAttr ".rnd" no;
	setAttr ".coi" 100.1;
	setAttr ".ow" 90.589101505197007;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "pCylinder1";
	setAttr ".s" -type "double3" 11.473825450228881 1 11.473825450228881 ;
createNode mesh -n "pCylinderShape1" -p "pCylinder1";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.43679553270339966 0.71970424056053162 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 7 ".pt";
	setAttr ".pt[31]" -type "float3" 0 2.6798325 0 ;
	setAttr ".pt[105]" -type "float3" 0 0.70457727 0 ;
	setAttr ".pt[106]" -type "float3" 0 -1.1301339 0 ;
	setAttr ".pt[107]" -type "float3" 0 -1.1301339 0 ;
	setAttr ".pt[108]" -type "float3" 0 -1.1301339 0 ;
	setAttr ".pt[117]" -type "float3" 0 -1.1301339 0 ;
	setAttr ".pt[119]" -type "float3" 0 -1.1032653 0 ;
createNode transform -n "imagePlane1";
	setAttr ".t" -type "double3" -15.240127232303255 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
	setAttr ".s" -type "double3" 3.1370944742117692 3.1370944742117692 3.1370944742117692 ;
createNode imagePlane -n "imagePlaneShape1" -p "imagePlane1";
	setAttr -k off ".v";
	setAttr ".fc" 51;
	setAttr ".imn" -type "string" "/Users/MisCosas/Documents/LAD_ITESM/7mo semestre/Props//sourceimages/maxresdefault.jpg";
	setAttr ".cov" -type "short2" 2163 1186 ;
	setAttr ".dlc" no;
	setAttr ".w" 21.63;
	setAttr ".h" 11.860000000000001;
createNode transform -n "pCube1";
	setAttr ".t" -type "double3" -46.846932331934326 0 0 ;
	setAttr ".s" -type "double3" 9.275987299725319 9.275987299725319 9.275987299725319 ;
createNode mesh -n "pCubeShape1" -p "pCube1";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.30846354365348816 0.375 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 18 ".pt";
	setAttr ".pt[0]" -type "float3" -0.12860972 0.078349054 -3.3861802e-15 ;
	setAttr ".pt[2]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[3]" -type "float3" -0.12860972 0.29052553 -0.39950493 ;
	setAttr ".pt[5]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[7]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[8]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[9]" -type "float3" 0 0.29052553 -0.39950493 ;
	setAttr ".pt[10]" -type "float3" 0 0.078349054 -3.3861802e-15 ;
	setAttr ".pt[13]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[15]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[17]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[19]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[22]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[23]" -type "float3" 0 0 -0.39950493 ;
	setAttr ".pt[24]" -type "float3" -0.16946861 0.23131874 -0.39950493 ;
	setAttr ".pt[25]" -type "float3" 0 0.23131874 -0.39950493 ;
	setAttr ".pt[26]" -type "float3" 0 0.014208851 -3.1086245e-15 ;
	setAttr ".pt[27]" -type "float3" -0.16946861 0.014208851 -3.1086245e-15 ;
createNode transform -n "imagePlane2";
	setAttr ".t" -type "double3" -36.149585530920604 7.1725368116905983 -18.570066031511864 ;
	setAttr ".s" -type "double3" 2.9635371137159634 2.9635371137159634 2.9635371137159634 ;
createNode imagePlane -n "imagePlaneShape2" -p "imagePlane2";
	setAttr -k off ".v";
	setAttr ".fc" 51;
	setAttr ".imn" -type "string" "/Users/MisCosas/Documents/ProjectKarts/Assets/Eduard/Armando Assets/hot-rod-v8-engine-3d-render-HA5Y24.jpg";
	setAttr ".cov" -type "short2" 1300 1048 ;
	setAttr ".dlc" no;
	setAttr ".w" 13;
	setAttr ".h" 10.479999999999999;
createNode transform -n "pCylinder2";
	setAttr ".t" -type "double3" -47.545681488944467 11.870562525803129 -7.5174864098490586 ;
createNode mesh -n "pCylinderShape2" -p "pCylinder2";
	setAttr -k off ".v";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".pv" -type "double2" 0.49999988079071045 0.50046992301940918 ;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 61 ".pt[81:141]" -type "float3"  0.32029316 0 -0.10406947 
		0.27245715 0 -0.19795193 0.27245715 0 -0.19795193 0.32029316 0 -0.10406947 0.19795169 
		0 -0.27245739 0.19795169 0 -0.27245739 0.1040694 0 -0.32029298 0.1040694 0 -0.32029298 
		0 0 -0.33677578 0 0 -0.33677578 -0.1040694 0 -0.32029295 -0.1040694 0 -0.32029295 
		-0.19795169 0 -0.27245736 -0.19795169 0 -0.27245736 -0.27245715 0 -0.19795193 -0.27245715 
		0 -0.19795193 -0.32029277 0 -0.10406946 -0.32029277 0 -0.10406946 -0.33677599 0 1.7610999e-08 
		-0.33677599 0 1.7610999e-08 -0.32029277 0 0.10406948 -0.32029277 0 0.10406948 -0.27245715 
		0 0.19795193 -0.27245715 0 0.19795193 -0.19795169 0 0.27245733 -0.19795169 0 0.27245733 
		-0.1040694 0 0.32029283 -0.1040694 0 0.32029283 0 0 0.33677578 0 0 0.33677578 0.1040694 
		0 0.32029283 0.1040694 0 0.32029283 0.19795169 0 0.27245733 0.19795169 0 0.27245733 
		0.27245715 0 0.1979519 0.27245715 0 0.1979519 0.32029277 0 0.10406946 0.32029277 
		0 0.10406946 0.33677599 0 1.7610999e-08 0.33677599 0 1.7610999e-08 -1.2887218 -3.375078e-14 
		0.41873094 -1.0962517 -3.375078e-14 0.7964735 0 -3.375078e-14 -7.0859109e-08 -0.79647332 
		-3.375078e-14 1.0962518 -0.41873083 -3.375078e-14 1.2887217 0 -3.375078e-14 1.3550414 
		0.41873083 -3.375078e-14 1.2887217 0.79647332 -3.375078e-14 1.0962517 1.0962517 -3.375078e-14 
		0.7964735 1.2887206 -3.375078e-14 0.41873088 1.3550413 -3.375078e-14 -7.0859109e-08 
		1.2887206 -3.375078e-14 -0.41873094 1.0962517 -3.375078e-14 -0.7964735 0.79647332 
		-3.375078e-14 -1.0962516 0.41873083 -3.375078e-14 -1.2887211 0 -3.375078e-14 -1.3550414 
		-0.41873083 -3.375078e-14 -1.2887211 -0.79647332 -3.375078e-14 -1.0962516 -1.0962517 
		-3.375078e-14 -0.79647344 -1.2887206 -3.375078e-14 -0.41873091 -1.3550413 -3.375078e-14 
		-7.0859109e-08;
createNode lightLinker -s -n "lightLinker1";
	setAttr -s 2 ".lnk";
	setAttr -s 2 ".slnk";
createNode displayLayerManager -n "layerManager";
createNode displayLayer -n "defaultLayer";
createNode renderLayerManager -n "renderLayerManager";
createNode renderLayer -n "defaultRenderLayer";
	setAttr ".g" yes;
createNode script -n "uiConfigurationScriptNode";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n"
		+ "                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n"
		+ "                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n"
		+ "            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n"
		+ "            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n"
		+ "                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n"
		+ "                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n"
		+ "            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n"
		+ "            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n"
		+ "            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n"
		+ "                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n"
		+ "                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n"
		+ "                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n"
		+ "            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n"
		+ "            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n"
		+ "            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n"
		+ "                -bufferMode \"double\" \n                -twoSidedLighting 0\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 16384\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"vp2Renderer\" \n                -objectFilterShowInHUD 1\n                -isFiltered 0\n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n"
		+ "                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -imagePlane 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -particleInstancers 1\n                -fluids 1\n                -hairSystems 1\n"
		+ "                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -pluginShapes 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -clipGhosts 1\n                -greasePencils 1\n                -shadows 0\n                $editorName;\n            modelEditor -e -viewSelected 0 $editorName;\n            modelEditor -e \n                -pluginObjects \"gpuCacheDisplayFilter\" 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n"
		+ "            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 0\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 16384\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"vp2Renderer\" \n            -objectFilterShowInHUD 1\n            -isFiltered 0\n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n"
		+ "            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -imagePlane 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -particleInstancers 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n"
		+ "            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -pluginShapes 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -clipGhosts 1\n            -greasePencils 1\n            -shadows 0\n            $editorName;\n        modelEditor -e -viewSelected 0 $editorName;\n        modelEditor -e \n            -pluginObjects \"gpuCacheDisplayFilter\" 1 \n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -docTag \"isolOutln_fromSeln\" \n                -showShapes 0\n"
		+ "                -showReferenceNodes 1\n                -showReferenceMembers 1\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n"
		+ "                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                -ignoreHiddenAttribute 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -docTag \"isolOutln_fromSeln\" \n"
		+ "            -showShapes 0\n            -showReferenceNodes 1\n            -showReferenceMembers 1\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n"
		+ "            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            -ignoreHiddenAttribute 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n"
		+ "            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n"
		+ "                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n"
		+ "                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n"
		+ "                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n"
		+ "                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n"
		+ "            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n"
		+ "                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n"
		+ "                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showReferenceNodes 0\n                -showReferenceMembers 0\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n"
		+ "                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n"
		+ "                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                -ignoreHiddenAttribute 0\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n"
		+ "                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n"
		+ "                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n"
		+ "\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n"
		+ "                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showConnectionFromSelected 0\n                -showConnectionToSelected 0\n                -showConstraintLabels 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n"
		+ "                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"nodeEditorPanel\" (localizedPanelLabel(\"Node Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"nodeEditorPanel\" -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n"
		+ "                -defaultPinnedState 0\n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -keyReleaseCommand \"nodeEdKeyReleaseCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Node Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"NodeEditorEd\");\n            nodeEditor -e \n                -allAttributes 0\n                -allNodes 0\n"
		+ "                -autoSizeNodes 1\n                -createNodeCommand \"nodeEdCreateNodeCommand\" \n                -defaultPinnedState 0\n                -ignoreAssets 1\n                -additiveGraphingMode 0\n                -settingsChangedCallback \"nodeEdSyncControls\" \n                -traversalDepthLimit -1\n                -keyPressCommand \"nodeEdKeyPressCommand\" \n                -keyReleaseCommand \"nodeEdKeyReleaseCommand\" \n                -nodeTitleMode \"name\" \n                -gridSnap 0\n                -gridVisibility 1\n                -popupMenuScript \"nodeEdBuildPanelMenus\" \n                -showNamespace 1\n                -showShapes 1\n                -showSGShapes 0\n                -showTransforms 1\n                -useAssets 1\n                -syncedSelection 1\n                -extendToShapes 1\n                $editorName;;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Texture Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n"
		+ "\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n"
		+ "        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n\t\t\t\t-defaultImage \"vacantCell.xP:/\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 0\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 16384\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"vp2Renderer\\\" \\n    -objectFilterShowInHUD 1\\n    -isFiltered 0\\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -imagePlane 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -particleInstancers 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -pluginShapes 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -clipGhosts 1\\n    -greasePencils 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName;\\nmodelEditor -e \\n    -pluginObjects \\\"gpuCacheDisplayFilter\\\" 1 \\n    $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 5 -size 12 -divisions 5 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 120 -ast 1 -aet 200 ";
	setAttr ".st" 6;
createNode polyCylinder -n "polyCylinder1";
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	setAttr ".ics" -type "componentList" 1 "f[40:59]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -1.3677866e-06 1 -2.0516798e-06 ;
	setAttr ".rs" 401460498;
	setAttr ".lt" -type "double3" 0 -3.7163062970508531e-17 1.8326324434540715 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -11.473828185802041 1 -11.473830921375203 ;
	setAttr ".cbx" -type "double3" 11.473825450228881 1 11.473826818015462 ;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	setAttr ".ics" -type "componentList" 1 "f[40:59]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -1.7097332e-06 2.8326325 3.9142194 ;
	setAttr ".rs" 1520159768;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -11.473828185802041 2.8326325416564941 -11.473830921375203 ;
	setAttr ".cbx" -type "double3" 11.473824766335591 2.8326325416564941 19.302269875675631 ;
createNode polyTweak -n "polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 27 ".tk";
	setAttr ".tk[10]" -type "float3" 0 0 0.057683751 ;
	setAttr ".tk[11]" -type "float3" 0.11236361 0 0.14810273 ;
	setAttr ".tk[12]" -type "float3" 0 0 0.54813147 ;
	setAttr ".tk[13]" -type "float3" 0 0 0.66060477 ;
	setAttr ".tk[14]" -type "float3" 0 0 0.68228716 ;
	setAttr ".tk[15]" -type "float3" 0 0 0.66060483 ;
	setAttr ".tk[16]" -type "float3" 0 0 0.54813153 ;
	setAttr ".tk[17]" -type "float3" -0.11236361 0 0.14810276 ;
	setAttr ".tk[18]" -type "float3" 0 0 0.057683781 ;
	setAttr ".tk[30]" -type "float3" 0 0 0.057683751 ;
	setAttr ".tk[31]" -type "float3" 0.11236361 0 0.14810273 ;
	setAttr ".tk[32]" -type "float3" 0 0 0.54813147 ;
	setAttr ".tk[33]" -type "float3" 0 0 0.66060477 ;
	setAttr ".tk[34]" -type "float3" 0 0 0.68228716 ;
	setAttr ".tk[35]" -type "float3" 0 0 0.66060483 ;
	setAttr ".tk[36]" -type "float3" 0 0 0.54813153 ;
	setAttr ".tk[37]" -type "float3" -0.11236361 0 0.14810276 ;
	setAttr ".tk[38]" -type "float3" 0 0 0.057683781 ;
	setAttr ".tk[52]" -type "float3" 0 0 0.057683751 ;
	setAttr ".tk[53]" -type "float3" 0.11236361 0 0.14810273 ;
	setAttr ".tk[54]" -type "float3" 0 0 0.54813147 ;
	setAttr ".tk[55]" -type "float3" 0 0 0.66060477 ;
	setAttr ".tk[56]" -type "float3" 0 0 0.68228716 ;
	setAttr ".tk[57]" -type "float3" 0 0 0.66060483 ;
	setAttr ".tk[58]" -type "float3" 0 0 0.54813153 ;
	setAttr ".tk[59]" -type "float3" -0.11236361 0 0.14810276 ;
	setAttr ".tk[60]" -type "float3" 0 0 0.057683781 ;
createNode polyExtrudeFace -n "polyExtrudeFace3";
	setAttr ".ics" -type "componentList" 1 "f[40:59]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".mp" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".pvt" -type "float3" -1.7097332e-06 2.8326325 3.9142194 ;
	setAttr ".rs" 1520159768;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -11.473828185802041 2.8326325416564941 -11.473830921375203 ;
	setAttr ".cbx" -type "double3" 11.473824766335591 2.8326325416564941 19.302269875675631 ;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	setAttr ".ics" -type "componentList" 1 "f[80:99]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -2.0516798e-06 2.8326325 3.9142189 ;
	setAttr ".rs" 140601784;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -11.473828185802041 2.8326325416564941 -11.473830921375203 ;
	setAttr ".cbx" -type "double3" 11.473824082442301 2.8326325416564941 19.30226850788905 ;
createNode polyTweak -n "polyTweak2";
	setAttr ".uopa" yes;
	setAttr -s 21 ".tk[61:81]" -type "float3"  -0.19276509 0 0.13177778 -0.16397579
		 0 0.18827996 -3.6242909e-08 0 0.069144644 -0.11913537 0 0.2331204 -0.062633194 0
		 0.26190963 -3.6242909e-08 0 0.27182981 0.062633112 0 0.26190963 0.11913528 0 0.2331204
		 0.16397561 0 0.18827993 0.19276494 0 0.13177775 0.20268507 0 0.069144644 0.19276494
		 0 -0.0051801265 0.1412012 0 -0.080008879 0.11913525 0 -0.20592912 0.062633105 0 -0.25751498
		 -3.0202429e-08 0 -0.27182981 -0.062633157 0 -0.25751498 -0.11913531 0 -0.20592909
		 -0.14120121 0 -0.08000885 -0.192765 0 -0.0051801084 -0.20268507 0 0.069144644;
createNode polyTweak -n "polyTweak3";
	setAttr ".uopa" yes;
	setAttr -s 76 ".tk";
	setAttr ".tk[10]" -type "float3" 0.11095662 0 0.50027925 ;
	setAttr ".tk[11]" -type "float3" -0.2165128 -0.13447194 0.47972554 ;
	setAttr ".tk[12]" -type "float3" -0.29470885 0 0.26583144 ;
	setAttr ".tk[13]" -type "float3" -0.39905548 0 0.41606763 ;
	setAttr ".tk[14]" -type "float3" 0.013896658 0 0.54113746 ;
	setAttr ".tk[15]" -type "float3" 0.39905548 0 0.41606763 ;
	setAttr ".tk[16]" -type "float3" 0.29470885 0 0.26583156 ;
	setAttr ".tk[17]" -type "float3" 0.25965995 -0.13447194 0.47972566 ;
	setAttr ".tk[18]" -type "float3" -0.11095662 0 0.50027925 ;
	setAttr ".tk[30]" -type "float3" 0.11095662 0 0.50027925 ;
	setAttr ".tk[31]" -type "float3" -0.2165128 -0.13447194 0.47972554 ;
	setAttr ".tk[32]" -type "float3" -0.29470885 0 0.26583144 ;
	setAttr ".tk[33]" -type "float3" -0.39905548 0 0.41606763 ;
	setAttr ".tk[34]" -type "float3" 0.013896658 0 0.54113746 ;
	setAttr ".tk[35]" -type "float3" 0.39905548 0 0.41606763 ;
	setAttr ".tk[36]" -type "float3" 0.29470885 0 0.26583156 ;
	setAttr ".tk[37]" -type "float3" 0.25965995 -0.13447194 0.47972566 ;
	setAttr ".tk[38]" -type "float3" -0.11095662 0 0.50027925 ;
	setAttr ".tk[51]" -type "float3" 0.11095662 0 0.50027925 ;
	setAttr ".tk[52]" -type "float3" -0.2165128 -0.13447194 0.47972554 ;
	setAttr ".tk[53]" -type "float3" -0.29470885 0 0.26583144 ;
	setAttr ".tk[54]" -type "float3" -0.39905548 0 0.41606763 ;
	setAttr ".tk[55]" -type "float3" 0.013896658 0 0.54113746 ;
	setAttr ".tk[56]" -type "float3" 0.39905548 0 0.41606763 ;
	setAttr ".tk[57]" -type "float3" 0.29470885 0 0.26583156 ;
	setAttr ".tk[58]" -type "float3" 0.25965995 -0.13447194 0.47972566 ;
	setAttr ".tk[59]" -type "float3" -0.11095662 0 0.50027925 ;
	setAttr ".tk[72]" -type "float3" 0.088467374 0 0.50027925 ;
	setAttr ".tk[73]" -type "float3" -0.16825627 -0.13447194 0.56063557 ;
	setAttr ".tk[74]" -type "float3" -0.23497584 0 0.47170198 ;
	setAttr ".tk[75]" -type "float3" -0.27661517 0 0.49944752 ;
	setAttr ".tk[76]" -type "float3" 0.013896649 0 0.54113746 ;
	setAttr ".tk[77]" -type "float3" 0.27661505 0 0.49944752 ;
	setAttr ".tk[78]" -type "float3" 0.23497577 0 0.47170198 ;
	setAttr ".tk[79]" -type "float3" 0.21140341 -0.13447194 0.56063569 ;
	setAttr ".tk[80]" -type "float3" -0.08846736 0 0.50027925 ;
	setAttr ".tk[82]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[83]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[84]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[85]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[86]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[87]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[88]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[89]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[90]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[91]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[92]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[93]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[94]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[95]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[96]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[97]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[98]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[99]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[100]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[101]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[102]" -type "float3" 0.11095662 3.9140053 0.58809459 ;
	setAttr ".tk[103]" -type "float3" 0.088467374 3.9140053 0.58809459 ;
	setAttr ".tk[104]" -type "float3" -0.2165128 5.4117966 0.47972554 ;
	setAttr ".tk[105]" -type "float3" -0.16825627 5.4117966 0.56063557 ;
	setAttr ".tk[106]" -type "float3" -0.29470885 10.017931 0.26583144 ;
	setAttr ".tk[107]" -type "float3" -0.23497584 10.017931 0.47170198 ;
	setAttr ".tk[108]" -type "float3" -0.39905548 10.017931 0.41606763 ;
	setAttr ".tk[109]" -type "float3" -0.27661517 10.017931 0.49944752 ;
	setAttr ".tk[110]" -type "float3" 0.013896658 10.017931 0.54113746 ;
	setAttr ".tk[111]" -type "float3" 0.013896649 10.017931 0.54113746 ;
	setAttr ".tk[112]" -type "float3" 0.39905548 10.017931 0.41606763 ;
	setAttr ".tk[113]" -type "float3" 0.27661505 10.017931 0.49944752 ;
	setAttr ".tk[114]" -type "float3" 0.29470885 10.017931 0.26583156 ;
	setAttr ".tk[115]" -type "float3" 0.23497577 10.017931 0.47170198 ;
	setAttr ".tk[116]" -type "float3" 0.25965995 5.4117966 0.47972566 ;
	setAttr ".tk[117]" -type "float3" 0.21140341 5.4117966 0.56063569 ;
	setAttr ".tk[118]" -type "float3" -0.11095662 3.9140053 0.58809459 ;
	setAttr ".tk[119]" -type "float3" -0.08846736 3.9140053 0.58809459 ;
	setAttr ".tk[120]" -type "float3" 0 10.017931 0 ;
	setAttr ".tk[121]" -type "float3" 0 10.017931 0 ;
createNode deleteComponent -n "deleteComponent1";
	setAttr ".dc" -type "componentList" 7 "f[0:3]" "f[14:23]" "f[34:43]" "f[54:63]" "f[74:83]" "f[94:107]" "f[128:139]";
createNode polySplitRing -n "polySplitRing1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[84]" "e[87]" "e[92]" "e[97]" "e[102]" "e[107]" "e[112]" "e[117]" "e[122]" "e[127]" "e[132]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".wt" 0.47524985671043396;
	setAttr ".dr" no;
	setAttr ".re" 92;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyTweak -n "polyTweak4";
	setAttr ".uopa" yes;
	setAttr -s 42 ".tk";
	setAttr ".tk[0]" -type "float3" 0 2.0865927 0.00044635125 ;
	setAttr ".tk[1]" -type "float3" 0 1.6257789 0.021546721 ;
	setAttr ".tk[2]" -type "float3" -0.10941473 1.1867774 0.069766417 ;
	setAttr ".tk[3]" -type "float3" 0.026798397 0.68913698 0.067820363 ;
	setAttr ".tk[4]" -type "float3" 0.14490855 0.77451354 0.18073206 ;
	setAttr ".tk[5]" -type "float3" 0.13552228 0 0.74100453 ;
	setAttr ".tk[6]" -type "float3" 0 0 0.042426273 ;
	setAttr ".tk[11]" -type "float3" 0 3.2409635 -0.24922624 ;
	setAttr ".tk[12]" -type "float3" 0 3.2409635 -0.24922624 ;
	setAttr ".tk[13]" -type "float3" -0.10941473 3.0842149 -0.22762984 ;
	setAttr ".tk[14]" -type "float3" -0.27899781 2.6385715 -0.1623736 ;
	setAttr ".tk[15]" -type "float3" -0.041812096 1.6805826 0.1121453 ;
	setAttr ".tk[16]" -type "float3" 0.13552228 0 0.74100453 ;
	setAttr ".tk[17]" -type "float3" 0 0 0.042426273 ;
	setAttr ".tk[23]" -type "float3" -0.035732709 6.8240004 -0.30486763 ;
	setAttr ".tk[24]" -type "float3" -0.035732709 6.7212143 -0.30569434 ;
	setAttr ".tk[25]" -type "float3" -0.14514743 6.4808545 -0.33469835 ;
	setAttr ".tk[26]" -type "float3" -0.29609776 5.9452486 -0.18124218 ;
	setAttr ".tk[27]" -type "float3" -0.11849233 4.0706234 0.19962759 ;
	setAttr ".tk[28]" -type "float3" 0.13552228 0 0.74100453 ;
	setAttr ".tk[29]" -type "float3" 0 0 0.042426273 ;
	setAttr ".tk[34]" -type "float3" 0 0 0.62162155 ;
	setAttr ".tk[38]" -type "float3" -0.1791884 0 0.097384989 ;
	setAttr ".tk[39]" -type "float3" -0.041812096 -1.080025e-12 0.36137152 ;
	setAttr ".tk[40]" -type "float3" 0.13552228 0 0.66913414 ;
	setAttr ".tk[41]" -type "float3" 0 0 0.047745474 ;
	setAttr ".tk[46]" -type "float3" 0 3.5213876 -0.29275721 ;
	setAttr ".tk[47]" -type "float3" 0 3.46593 -0.29275721 ;
	setAttr ".tk[48]" -type "float3" -0.062819727 2.8909605 -0.26762933 ;
	setAttr ".tk[49]" -type "float3" -0.062819727 2.8909605 -0.26762933 ;
	setAttr ".tk[50]" -type "float3" -0.14368632 1.3533056 -0.27274343 ;
	setAttr ".tk[51]" -type "float3" -0.14368632 1.3533056 -0.27274343 ;
	setAttr ".tk[52]" -type "float3" -0.20759574 -0.99813467 -0.015371151 ;
	setAttr ".tk[53]" -type "float3" -0.20759574 -0.99813467 -0.015371151 ;
	setAttr ".tk[54]" -type "float3" -0.11567996 -2.228312 0.33970654 ;
	setAttr ".tk[55]" -type "float3" -0.11567996 -2.228312 0.33970654 ;
	setAttr ".tk[56]" -type "float3" 0.13552228 -8.2661505 0.5735029 ;
	setAttr ".tk[57]" -type "float3" 0.13552228 -8.2661505 0.50163257 ;
	setAttr ".tk[58]" -type "float3" 0.023493946 -2.3560765 0.062557057 ;
	setAttr ".tk[59]" -type "float3" 0.023493946 -2.3560765 0.067876264 ;
	setAttr ".tk[60]" -type "float3" 0.014235892 1.4566126e-13 0.041423284 ;
	setAttr ".tk[61]" -type "float3" 0.014235892 1.4566126e-13 0.041423284 ;
createNode polyExtrudeFace -n "polyExtrudeFace5";
	setAttr ".ics" -type "componentList" 1 "f[70:79]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -6.0485182 9.9724941 5.3026967 ;
	setAttr ".rs" 1373020293;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -12.256483873798629 3.5730376243591309 -14.905790734878778 ;
	setAttr ".cbx" -type "double3" 0.15944774372542928 16.371950149536133 25.511183865084071 ;
createNode polyTweak -n "polyTweak5";
	setAttr ".uopa" yes;
	setAttr -s 74 ".tk";
	setAttr ".tk[6]" -type "float3" 0 0 -0.019579165 ;
	setAttr ".tk[7]" -type "float3" 0.14967354 0 -0.063041516 ;
	setAttr ".tk[8]" -type "float3" 0.14649181 0 -0.23178232 ;
	setAttr ".tk[9]" -type "float3" 0.12023807 0 -0.28923142 ;
	setAttr ".tk[10]" -type "float3" 0.011568114 0 -0.31700802 ;
	setAttr ".tk[13]" -type "float3" 0 0 -0.024261618 ;
	setAttr ".tk[14]" -type "float3" 0 0 0.022822458 ;
	setAttr ".tk[15]" -type "float3" 0 0 0.081671819 ;
	setAttr ".tk[17]" -type "float3" 0 0 -0.019579165 ;
	setAttr ".tk[18]" -type "float3" 0.14967354 0 -0.063041516 ;
	setAttr ".tk[19]" -type "float3" 0.14649181 0 -0.23178232 ;
	setAttr ".tk[20]" -type "float3" 0.12023807 0 -0.28923142 ;
	setAttr ".tk[21]" -type "float3" 0.011568114 0 -0.31700802 ;
	setAttr ".tk[25]" -type "float3" 0 0 0.079955868 ;
	setAttr ".tk[26]" -type "float3" 0 0 0.043781653 ;
	setAttr ".tk[27]" -type "float3" 0 0 -0.011085295 ;
	setAttr ".tk[29]" -type "float3" 0 0 -0.019579165 ;
	setAttr ".tk[30]" -type "float3" 0 0 -0.063041516 ;
	setAttr ".tk[31]" -type "float3" 0 0 -0.12086216 ;
	setAttr ".tk[32]" -type "float3" 0 0 -0.17831129 ;
	setAttr ".tk[33]" -type "float3" 0 0 -0.20608787 ;
	setAttr ".tk[41]" -type "float3" 0 0 -0.019598901 ;
	setAttr ".tk[42]" -type "float3" 0 0 -0.06316942 ;
	setAttr ".tk[43]" -type "float3" 0 0 -0.12085386 ;
	setAttr ".tk[44]" -type "float3" 0 0 -0.15359499 ;
	setAttr ".tk[45]" -type "float3" 0 0 -0.16750497 ;
	setAttr ".tk[46]" -type "float3" 0 0.54486793 0 ;
	setAttr ".tk[48]" -type "float3" 1.8626451e-09 0.54486793 -2.9802322e-08 ;
	setAttr ".tk[50]" -type "float3" 3.7252903e-09 0.54486793 2.9802322e-08 ;
	setAttr ".tk[52]" -type "float3" -7.4505806e-09 0.54486793 -1.4901161e-08 ;
	setAttr ".tk[54]" -type "float3" 0.018441411 0.54486793 -0.16221619 ;
	setAttr ".tk[55]" -type "float3" 0.018441411 4.2632564e-14 -0.29339579 ;
	setAttr ".tk[58]" -type "float3" 0 0 -0.034900781 ;
	setAttr ".tk[59]" -type "float3" 0 0 -0.034920532 ;
	setAttr ".tk[60]" -type "float3" 0 0 -0.068921044 ;
	setAttr ".tk[61]" -type "float3" 0 0 -0.069048971 ;
	setAttr ".tk[62]" -type "float3" 7.4505806e-09 0 -0.12086218 ;
	setAttr ".tk[63]" -type "float3" 0 0 -0.12085386 ;
	setAttr ".tk[64]" -type "float3" 0 0 -0.17831129 ;
	setAttr ".tk[65]" -type "float3" 0 0 -0.15359499 ;
	setAttr ".tk[66]" -type "float3" 2.2351742e-08 0 -0.2060879 ;
	setAttr ".tk[67]" -type "float3" 0 0 -0.16750497 ;
	setAttr ".tk[68]" -type "float3" 2.2351742e-08 0 -0.2060879 ;
	setAttr ".tk[69]" -type "float3" 0 0 -0.17831129 ;
	setAttr ".tk[70]" -type "float3" 7.4505806e-09 0 -0.12086218 ;
	setAttr ".tk[71]" -type "float3" 0 0 -0.065835752 ;
	setAttr ".tk[72]" -type "float3" 0 0 -0.026860744 ;
	setAttr ".tk[74]" -type "float3" 7.4505806e-09 0 -0.081671819 ;
	setAttr ".tk[75]" -type "float3" 0 0 -0.043781634 ;
	setAttr ".tk[76]" -type "float3" -3.7252903e-09 0 0.049259804 ;
	setAttr ".tk[77]" -type "float3" 0 0 2.9802322e-08 ;
	setAttr ".tk[78]" -type "float3" 0 0 2.9802322e-08 ;
	setAttr ".tk[79]" -type "float3" 0 -0.21524909 -0.069340073 ;
	setAttr ".tk[80]" -type "float3" -0.11212251 -0.21524909 -0.10851913 ;
	setAttr ".tk[81]" -type "float3" 0 0.26042166 -0.069340073 ;
	setAttr ".tk[82]" -type "float3" -0.11212251 0.26042166 -0.10851913 ;
	setAttr ".tk[83]" -type "float3" -0.11212251 -0.21524909 -0.082495369 ;
	setAttr ".tk[84]" -type "float3" -0.11212251 0.26042166 -0.082495369 ;
	setAttr ".tk[85]" -type "float3" -0.11755188 -0.41971123 -0.021636344 ;
	setAttr ".tk[86]" -type "float3" -0.11755188 -0.1563637 -0.023033902 ;
	setAttr ".tk[87]" -type "float3" -0.16579922 -0.057841264 0.011847618 ;
	setAttr ".tk[88]" -type "float3" -0.16579922 0.016132157 0.0082055805 ;
	setAttr ".tk[89]" -type "float3" -0.16579922 -0.049505107 -0.026946034 ;
	setAttr ".tk[90]" -type "float3" -0.16579922 0.033672739 0.055049747 ;
	setAttr ".tk[91]" -type "float3" -0.077505812 -0.11779189 -0.089131579 ;
	setAttr ".tk[92]" -type "float3" -0.077505812 0.058793232 -0.16227417 ;
	setAttr ".tk[93]" -type "float3" -0.095947251 0.024120178 -0.078000844 ;
	setAttr ".tk[94]" -type "float3" -0.15898797 0.17010561 -0.070124984 ;
	setAttr ".tk[95]" -type "float3" 0 0.15067047 -0.1426428 ;
	setAttr ".tk[96]" -type "float3" 0 0.38287532 -0.13970105 ;
	setAttr ".tk[97]" -type "float3" 0 0.2282069 -0.15389816 ;
	setAttr ".tk[98]" -type "float3" 0 0.52200973 -0.15209073 ;
	setAttr ".tk[99]" -type "float3" 0 0.26019755 -0.15936816 ;
	setAttr ".tk[100]" -type "float3" 0 0.5790537 -0.15879306 ;
createNode deleteComponent -n "deleteComponent2";
	setAttr ".dc" -type "componentList" 1 "f[14:19]";
createNode polyExtrudeEdge -n "polyExtrudeEdge1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[4:8]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -8.33179 -0.67997915 9.237648 ;
	setAttr ".rs" 1166488704;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -9.9188695864355498 -1.1344718933105469 -1.4719197631533876 ;
	setAttr ".cbx" -type "double3" -6.7447101875966178 -0.22548645734786987 19.94721544297585 ;
createNode polyTweak -n "polyTweak6";
	setAttr ".uopa" yes;
	setAttr -s 6 ".tk[101:106]" -type "float3"  0.85735244 0 0 0.85735244
		 0 0 0.85735244 0 0 0.85735244 0 0 0.85735244 0 0 0.85735244 0 0;
createNode deleteComponent -n "deleteComponent3";
	setAttr ".dc" -type "componentList" 2 "f[75]" "f[95]";
createNode polyMergeVert -n "polyMergeVert1";
	setAttr ".ics" -type "componentList" 2 "vtx[22]" "vtx[101]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[22]";
createNode polyTweak -n "polyTweak7";
	setAttr ".uopa" yes;
	setAttr ".tk[101]" -type "float3"  0 0 0.26758039;
createNode polyMergeVert -n "polyMergeVert2";
	setAttr ".ics" -type "componentList" 2 "vtx[10]" "vtx[105]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[10]";
createNode polySplitRing -n "polySplitRing2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 11 "e[79]" "e[83]" "e[88]" "e[93]" "e[98]" "e[103]" "e[108]" "e[113]" "e[118]" "e[123]" "e[128]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".wt" 0.38145136833190918;
	setAttr ".re" 113;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyTweak -n "polyTweak8";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[39:40]" -type "float3"  0 0 -0.023017056 0 0 -0.13163245;
createNode deleteComponent -n "deleteComponent4";
	setAttr ".dc" -type "componentList" 1 "f[14:17]";
createNode deleteComponent -n "deleteComponent5";
	setAttr ".dc" -type "componentList" 1 "f[14]";
createNode polyExtrudeFace -n "polyExtrudeFace6";
	setAttr ".ics" -type "componentList" 3 "f[40]" "f[42]" "f[44]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.7907009 5.4043512 -6.5513463 ;
	setAttr ".rs" 150034540;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -9.5814010746117564 2.8326325416564941 -9.6362151218193404 ;
	setAttr ".cbx" -type "double3" -4.1584481303188644e-07 7.9760699272155762 -3.4664772756453242 ;
createNode deleteComponent -n "deleteComponent6";
	setAttr ".dc" -type "componentList" 1 "f[46]";
createNode polyTweak -n "polyTweak9";
	setAttr ".uopa" yes;
	setAttr -s 9 ".tk";
	setAttr ".tk[38]" -type "float3" 0 0 2.9802322e-08 ;
	setAttr ".tk[116]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[117]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[118]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[119]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[120]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[121]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[122]" -type "float3" 0 0 0.22812219 ;
	setAttr ".tk[123]" -type "float3" 0 0 0.22812219 ;
createNode deleteComponent -n "deleteComponent7";
	setAttr ".dc" -type "componentList" 3 "f[103:104]" "f[106]" "f[108]";
createNode deleteComponent -n "deleteComponent8";
	setAttr ".dc" -type "componentList" 1 "f[106]";
createNode polyMergeVert -n "polyMergeVert3";
	setAttr ".ics" -type "componentList" 2 "vtx[35]" "vtx[118]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[35]";
createNode polyTweak -n "polyTweak10";
	setAttr ".uopa" yes;
	setAttr -s 32 ".tk";
	setAttr ".tk[5]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[6]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[16]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[17]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[28]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[29]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[36]" -type "float3" 0.095651902 0 0.0085816737 ;
	setAttr ".tk[37]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[52]" -type "float3" 0.095651902 0 0.038705379 ;
	setAttr ".tk[53]" -type "float3" 0.095651902 0 0.041182689 ;
	setAttr ".tk[54]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[55]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[68]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[69]" -type "float3" 0.095651902 0 -0.041182693 ;
	setAttr ".tk[83]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[84]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[85]" -type "float3" 0.095651902 0 -0.016691783 ;
	setAttr ".tk[86]" -type "float3" 0.095651902 0 -0.011328668 ;
	setAttr ".tk[101]" -type "float3" 0 3.4988012 -0.059980113 ;
	setAttr ".tk[102]" -type "float3" 0 3.4988012 -0.059980113 ;
	setAttr ".tk[103]" -type "float3" 0 3.4988012 -0.059980113 ;
	setAttr ".tk[104]" -type "float3" 0 3.4988012 -0.059980113 ;
	setAttr ".tk[106]" -type "float3" 0.095651902 0 0.02101736 ;
	setAttr ".tk[107]" -type "float3" 0.095651902 0 0 ;
	setAttr ".tk[112]" -type "float3" 0 0 0.51457399 ;
	setAttr ".tk[113]" -type "float3" 0 0 0.47931612 ;
	setAttr ".tk[114]" -type "float3" 0 0 0.61547118 ;
	setAttr ".tk[115]" -type "float3" 0 0 0.57155311 ;
	setAttr ".tk[116]" -type "float3" 0 0 0.37699324 ;
	setAttr ".tk[117]" -type "float3" 0 0 0.47099268 ;
	setAttr ".tk[118]" -type "float3" 0 0 0.12963378 ;
	setAttr ".tk[119]" -type "float3" 0 0 0.16849458 ;
createNode polyExtrudeEdge -n "polyExtrudeEdge2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[58]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -3.2479014 2.8326325 7.4923754 ;
	setAttr ".rs" 1400345841;
	setAttr ".lt" -type "double3" -6.106226635438361e-16 2.7870185960600447 -3.501032810556727e-16 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -6.4958024373841301 2.8326325416564941 7.0590193370419012 ;
	setAttr ".cbx" -type "double3" -4.1584481303188644e-07 2.8326325416564941 7.9257309815474057 ;
createNode polyMergeVert -n "polyMergeVert4";
	setAttr ".ics" -type "componentList" 2 "vtx[35]" "vtx[119]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[35]";
createNode polyMergeVert -n "polyMergeVert5";
	setAttr ".ics" -type "componentList" 2 "vtx[116]" "vtx[119]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[116]";
createNode polyExtrudeEdge -n "polyExtrudeEdge3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[225]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -2.6886055 2.8326325 4.1304903 ;
	setAttr ".rs" 835454599;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -5.3772105004803885 2.8326325416564941 0.3352492567255082 ;
	setAttr ".cbx" -type "double3" -4.1584481303188644e-07 2.8326325416564941 7.9257309815474057 ;
createNode polyMergeVert -n "polyMergeVert6";
	setAttr ".ics" -type "componentList" 2 "vtx[113]" "vtx[120]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[113]";
createNode polyTweak -n "polyTweak11";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[119:120]" -type "float3"  0.16927537 0 0 0.16927537
		 0 0;
createNode polyMergeVert -n "polyMergeVert7";
	setAttr ".ics" -type "componentList" 2 "vtx[112]" "vtx[119]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[112]";
createNode deleteComponent -n "deleteComponent9";
	setAttr ".dc" -type "componentList" 1 "f[16:18]";
createNode deleteComponent -n "deleteComponent10";
	setAttr ".dc" -type "componentList" 1 "f[50]";
createNode polyExtrudeFace -n "polyExtrudeFace7";
	setAttr ".ics" -type "componentList" 2 "f[51]" "f[53]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -3.956917 4.743309 18.852396 ;
	setAttr ".rs" 287837587;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -8.0732809589028189 2.8326325416564941 17.234458330219777 ;
	setAttr ".cbx" -type "double3" 0.15944703846047378 6.6539859771728516 20.470332259564167 ;
createNode polyTweak -n "polyTweak12";
	setAttr ".uopa" yes;
	setAttr -s 6 ".tk[119:124]" -type "float3"  0.13025165 0 -0.35950431 0.064673372
		 0 -0.55507743 0.13025165 0 -0.35950431 0.064673372 0 -0.55507743 -0.13025163 0 -0.63816643
		 -0.13025163 0 -0.63816643;
createNode deleteComponent -n "deleteComponent11";
	setAttr ".dc" -type "componentList" 1 "f[109]";
createNode polyMergeVert -n "polyMergeVert8";
	setAttr ".ics" -type "componentList" 2 "vtx[108]" "vtx[121]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[108]";
createNode polyMergeVert -n "polyMergeVert9";
	setAttr ".ics" -type "componentList" 2 "vtx[38]" "vtx[119]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[38]";
createNode polyExtrudeEdge -n "polyExtrudeEdge4";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "e[62]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -4.1518531 2.7653966 10.579507 ;
	setAttr ".rs" 1805214563;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -8.3037058090550175 2.6981606483459473 7.9257309815474057 ;
	setAttr ".cbx" -type "double3" -4.1584481303188644e-07 2.8326325416564941 13.233283189010301 ;
createNode polyMergeVert -n "polyMergeVert10";
	setAttr ".ics" -type "componentList" 2 "vtx[119]" "vtx[123]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[119]";
createNode polyTweak -n "polyTweak13";
	setAttr ".uopa" yes;
	setAttr -s 2 ".tk[123:124]" -type "float3"  0.42757216 0 -0.15232909 0.42757216
		 0 -0.15232909;
createNode polyMergeVert -n "polyMergeVert11";
	setAttr ".ics" -type "componentList" 2 "vtx[121]" "vtx[123]";
	setAttr ".ix" -type "matrix" 11.473825450228881 0 0 0 0 1 0 0 0 0 11.473825450228881 0
		 0 0 0 1;
	setAttr ".mtc" -type "componentList" 1 "vtx[121]";
createNode deleteComponent -n "deleteComponent12";
	setAttr ".dc" -type "componentList" 1 "f[105]";
createNode deleteComponent -n "deleteComponent13";
	setAttr ".dc" -type "componentList" 2 "f[104]" "f[106]";
createNode polyCube -n "polyCube1";
	setAttr ".cuv" 4;
createNode polyExtrudeFace -n "polyExtrudeFace8";
	setAttr ".ics" -type "componentList" 1 "f[1]";
	setAttr ".ix" -type "matrix" 9.275987299725319 0 0 0 0 9.275987299725319 0 0 0 0 9.275987299725319 0
		 -46.846932331934326 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -46.846931 4.6379938 0 ;
	setAttr ".rs" 1748393553;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -51.484925981796984 4.6379936498626595 -4.3545605141527215 ;
	setAttr ".cbx" -type "double3" -42.208938682071668 4.6379936498626595 4.3545605141527215 ;
createNode polyTweak -n "polyTweak14";
	setAttr ".uopa" yes;
	setAttr -s 8 ".tk[0:7]" -type "float3"  2.9976022e-15 2.9976022e-15
		 -0.030555565 -2.9976022e-15 2.9976022e-15 -0.030555565 2.9976022e-15 -2.9976022e-15
		 -0.030555565 -2.9976022e-15 -2.9976022e-15 -0.030555565 2.9976022e-15 -2.9976022e-15
		 0.030555565 -2.9976022e-15 -2.9976022e-15 0.030555565 2.9976022e-15 2.9976022e-15
		 0.030555565 -2.9976022e-15 2.9976022e-15 0.030555565;
createNode polySplitRing -n "polySplitRing3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 3 "e[0:3]" "e[14]" "e[18]";
	setAttr ".ix" -type "matrix" 9.275987299725319 0 0 0 0 9.275987299725319 0 0 0 0 9.275987299725319 0
		 -46.846932331934326 0 0 1;
	setAttr ".wt" 0.46770840883255005;
	setAttr ".re" 14;
	setAttr ".sma" 29.999999999999996;
	setAttr ".p[0]"  0 0 1;
	setAttr ".fq" yes;
createNode polyTweak -n "polyTweak15";
	setAttr ".uopa" yes;
	setAttr -s 4 ".tk[8:11]" -type "float3"  0.23030274 0.24071246 -1.2767565e-15
		 -0.23030274 0.24071246 -1.2767565e-15 -0.23030274 0.24071246 1.2767565e-15 0.23030274
		 0.24071246 1.2767565e-15;
createNode deleteComponent -n "deleteComponent14";
	setAttr ".dc" -type "componentList" 3 "f[4]" "f[7]" "f[10:15]";
createNode polyExtrudeFace -n "polyExtrudeFace9";
	setAttr ".ics" -type "componentList" 1 "f[7]";
	setAttr ".ix" -type "matrix" 9.275987299725319 0 0 0 0 9.275987299725319 0 0 0 0 9.275987299725319 0
		 -46.846932331934326 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -52.462799 6.9567804 -1.6484963 ;
	setAttr ".rs" 1046323743;
	setAttr ".lt" -type "double3" 1.7763568394002505e-15 6.2910588343095155e-17 5.9768732127211948 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -53.931371030821886 5.8403577735016023 -7.6515532187830839 ;
	setAttr ".cbx" -type "double3" -50.994230525505706 8.0732035311130677 4.3545607905986854 ;
createNode polyTweak -n "polyTweak16";
	setAttr ".uopa" yes;
	setAttr -s 13 ".tk[0:12]" -type "float3"  0 0.35157931 -7.0499162e-15
		 -0.26373962 0.12962115 -7.2164497e-16 -0.26373962 0.12962115 -0.35543305 0 0.35057446
		 -0.35543305 -0.17740321 0.12962115 -7.2164497e-16 -0.17740321 0.12962115 -0.35543305
		 -0.061534669 0.12962115 -2.7200464e-15 -0.061534669 0.12962115 -0.35543305 -0.048778005
		 0.12962115 -0.35543305 -0.039626066 0.35057446 -0.35543305 -0.039626066 0.35157931
		 -9.0483177e-15 -0.048778005 0.12962115 -2.7200464e-15 0 0 0;
createNode polyExtrudeFace -n "polyExtrudeFace10";
	setAttr ".ics" -type "componentList" 1 "f[8:11]";
	setAttr ".ix" -type "matrix" 9.275987299725319 0 0 0 0 9.275987299725319 0 0 0 0 9.275987299725319 0
		 -46.846932331934326 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -54.271374 9.3358212 -1.6484965 ;
	setAttr ".rs" 4484185;
	setAttr ".ls" -type "double3" 1.0729824327727746 1.1666666744943646 1.1666666744943646 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -57.548513871204833 5.8403577735016023 -7.6515537716750117 ;
	setAttr ".cbx" -type "double3" -50.994232184181492 12.831283649048764 4.3545607905986854 ;
createNode polyExtrudeFace -n "polyExtrudeFace11";
	setAttr ".ics" -type "componentList" 1 "f[3]";
	setAttr ".ix" -type "matrix" 9.275987299725319 0 0 0 0 9.275987299725319 0 0 0 0 9.275987299725319 0
		 -46.846932331934326 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -49.495384 -0.37458214 -1.6484967 ;
	setAttr ".rs" 732296797;
	setAttr ".lt" -type "double3" 7.1054273576010019e-15 1.3877787807814457e-16 0.93103115633006706 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -51.47673046474911 -1.3860693778464936 -7.6515543245669395 ;
	setAttr ".cbx" -type "double3" -47.514038462781862 0.63690510039866 4.3545607905986854 ;
createNode polyTweak -n "polyTweak17";
	setAttr ".uopa" yes;
	setAttr -s 29 ".tk";
	setAttr ".tk[0]" -type "float3" 0.00088353083 0.2170824 0 ;
	setAttr ".tk[1]" -type "float3" -0.13115387 0.003142036 0 ;
	setAttr ".tk[2]" -type "float3" -0.13115387 0.003142036 0 ;
	setAttr ".tk[3]" -type "float3" 0.00088353083 0 0 ;
	setAttr ".tk[4]" -type "float3" -0.18148105 0.057862777 0 ;
	setAttr ".tk[5]" -type "float3" -0.18148105 0.057862777 0 ;
	setAttr ".tk[6]" -type "float3" 0 0.062963359 0 ;
	setAttr ".tk[7]" -type "float3" 0 0.062963359 0 ;
	setAttr ".tk[10]" -type "float3" 0 0.2170824 0 ;
	setAttr ".tk[12]" -type "float3" -0.10236108 -0.16987595 -0.084368363 ;
	setAttr ".tk[13]" -type "float3" -0.10236108 -0.16987595 0.026404329 ;
	setAttr ".tk[14]" -type "float3" -0.1805172 -0.1359828 -0.084368363 ;
	setAttr ".tk[15]" -type "float3" -0.1805172 -0.1359828 0.026404329 ;
	setAttr ".tk[16]" -type "float3" -0.17435832 0.045427781 0 ;
	setAttr ".tk[17]" -type "float3" -0.17435832 0.045427781 0 ;
	setAttr ".tk[18]" -type "float3" -0.042031001 -0.25349957 0.0045253467 ;
	setAttr ".tk[19]" -type "float3" -0.042031001 -0.25349957 0.0045253448 ;
	setAttr ".tk[20]" -type "float3" -0.22667335 0.10025692 0 ;
	setAttr ".tk[21]" -type "float3" -0.094346114 -0.1986701 0.0045253467 ;
	setAttr ".tk[22]" -type "float3" -0.22667335 0.10025692 0 ;
	setAttr ".tk[23]" -type "float3" -0.094346114 -0.1986701 0.0045253467 ;
	setAttr ".tk[24]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[25]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[26]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[27]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[28]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[29]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[30]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
	setAttr ".tk[31]" -type "float3" 2.9802322e-08 2.9802322e-08 -9.3132257e-10 ;
createNode deleteComponent -n "deleteComponent15";
	setAttr ".dc" -type "componentList" 1 "f[21]";
createNode polyCylinder -n "polyCylinder2";
	setAttr ".sc" 1;
	setAttr ".cuv" 3;
createNode polyExtrudeFace -n "polyExtrudeFace12";
	setAttr ".ics" -type "componentList" 1 "f[0:19]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -47.333921308385342 13.316532306440866 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -47.333923 13.316532 -2.3841858e-07 ;
	setAttr ".rs" 131097913;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -51.893209333287686 12.316532306440866 -4.5592880249023438 ;
	setAttr ".cbx" -type "double3" -42.774634237157315 14.316532306440866 4.5592875480651855 ;
createNode polyTweak -n "polyTweak18";
	setAttr ".uopa" yes;
	setAttr -s 42 ".tk[0:41]" -type "float3"  3.3850863 -1.4210855e-14 -1.099880815
		 2.87952518 -1.4210855e-14 -2.092097998 2.092097998 -1.4210855e-14 -2.87952518 1.099880815
		 -1.4210855e-14 -3.38508582 0 -1.4210855e-14 -3.55928779 -1.099880815 -1.4210855e-14
		 -3.38508582 -2.092097998 -1.4210855e-14 -2.87952471 -2.87952471 -1.4210855e-14 -2.092097998
		 -3.38508391 -1.4210855e-14 -1.099880695 -3.55928755 -1.4210855e-14 0 -3.38508391
		 -1.4210855e-14 1.099880695 -2.87952375 -1.4210855e-14 2.092097521 -2.092097521 -1.4210855e-14
		 2.87952375 -1.099880695 -1.4210855e-14 3.38508344 -1.0607502e-07 -1.4210855e-14 3.55928731
		 1.099880338 -1.4210855e-14 3.38508344 2.092096567 -1.4210855e-14 2.87952375 2.87952352
		 -1.4210855e-14 2.092097282 3.38508344 -1.4210855e-14 1.099880338 3.55928707 -1.4210855e-14
		 0 3.3850863 -1.4210855e-14 -1.099880815 2.87952518 -1.4210855e-14 -2.092097998 2.092097998
		 -1.4210855e-14 -2.87952518 1.099880815 -1.4210855e-14 -3.38508582 0 -1.4210855e-14
		 -3.55928779 -1.099880815 -1.4210855e-14 -3.38508582 -2.092097998 -1.4210855e-14 -2.87952471
		 -2.87952471 -1.4210855e-14 -2.092097998 -3.38508391 -1.4210855e-14 -1.099880695 -3.55928755
		 -1.4210855e-14 0 -3.38508391 -1.4210855e-14 1.099880695 -2.87952375 -1.4210855e-14
		 2.092097521 -2.092097521 -1.4210855e-14 2.87952375 -1.099880695 -1.4210855e-14 3.38508344
		 -1.0607502e-07 -1.4210855e-14 3.55928731 1.099880338 -1.4210855e-14 3.38508344 2.092096567
		 -1.4210855e-14 2.87952375 2.87952352 -1.4210855e-14 2.092097282 3.38508344 -1.4210855e-14
		 1.099880338 3.55928707 -1.4210855e-14 0 0 -1.4210855e-14 0 0 -1.4210855e-14 0;
createNode polyExtrudeFace -n "polyExtrudeFace13";
	setAttr ".ics" -type "componentList" 1 "f[0:19]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -47.333921308385342 13.316532306440866 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -47.33392 13.316532 -2.3841858e-07 ;
	setAttr ".rs" 633939128;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -51.893209333287686 12.681125454672372 -4.5592880249023438 ;
	setAttr ".cbx" -type "double3" -42.774633283482999 13.95193915820936 4.5592875480651855 ;
createNode polyTweak -n "polyTweak19";
	setAttr ".uopa" yes;
	setAttr -s 40 ".tk[42:81]" -type "float3"  0 0.36459318 0 0 0.36459318
		 0 0 -0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0
		 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318
		 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0
		 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318
		 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0
		 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318
		 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0 0 0.36459318 0 0 -0.36459318 0;
createNode polyExtrudeFace -n "polyExtrudeFace14";
	setAttr ".ics" -type "componentList" 1 "f[0:19]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -47.333921308385342 13.316532306440866 0 1;
	setAttr ".ws" yes;
	setAttr ".mp" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -47.333921308385342 13.316532306440866 0 1;
	setAttr ".pvt" -type "float3" -47.33392 13.316532 -2.3841858e-07 ;
	setAttr ".rs" 633939128;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -51.893209333287686 12.681125454672372 -4.5592880249023438 ;
	setAttr ".cbx" -type "double3" -42.774633283482999 13.95193915820936 4.5592875480651855 ;
createNode polyExtrudeFace -n "polyExtrudeFace15";
	setAttr ".ics" -type "componentList" 1 "f[20:39]";
	setAttr ".ix" -type "matrix" 1 0 0 0 0 1 0 0 0 0 1 0 -47.333921308385342 13.316532306440866 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" -47.33392 12.316532 -2.3841858e-07 ;
	setAttr ".rs" 1546745748;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -51.893209333287686 12.316532306440866 -4.5592880249023438 ;
	setAttr ".cbx" -type "double3" -42.774633283482999 12.316532306440866 4.5592875480651855 ;
createNode polyTweak -n "polyTweak20";
	setAttr ".uopa" yes;
	setAttr -s 41 ".tk";
	setAttr ".tk[82]" -type "float3" -0.77493781 -7.4505806e-08 0.2517924 ;
	setAttr ".tk[83]" -type "float3" -0.65920079 -7.4505806e-08 0.47893769 ;
	setAttr ".tk[84]" -type "float3" -0.65920079 7.4505806e-08 0.47893769 ;
	setAttr ".tk[85]" -type "float3" -0.77493781 7.4505806e-08 0.2517924 ;
	setAttr ".tk[86]" -type "float3" -0.47893757 -7.4505806e-08 0.65920115 ;
	setAttr ".tk[87]" -type "float3" -0.47893757 7.4505806e-08 0.65920115 ;
	setAttr ".tk[88]" -type "float3" -0.2517924 -7.4505806e-08 0.77493757 ;
	setAttr ".tk[89]" -type "float3" -0.2517924 7.4505806e-08 0.77493757 ;
	setAttr ".tk[90]" -type "float3" 0 -7.4505806e-08 0.81481719 ;
	setAttr ".tk[91]" -type "float3" 0 7.4505806e-08 0.81481719 ;
	setAttr ".tk[92]" -type "float3" 0.2517924 -7.4505806e-08 0.77493757 ;
	setAttr ".tk[93]" -type "float3" 0.2517924 7.4505806e-08 0.77493757 ;
	setAttr ".tk[94]" -type "float3" 0.47893757 -7.4505806e-08 0.65920091 ;
	setAttr ".tk[95]" -type "float3" 0.47893757 7.4505806e-08 0.65920091 ;
	setAttr ".tk[96]" -type "float3" 0.65920079 -7.4505806e-08 0.47893769 ;
	setAttr ".tk[97]" -type "float3" 0.65920079 7.4505806e-08 0.47893769 ;
	setAttr ".tk[98]" -type "float3" 0.77493697 -7.4505806e-08 0.25179234 ;
	setAttr ".tk[99]" -type "float3" 0.77493697 7.4505806e-08 0.25179234 ;
	setAttr ".tk[100]" -type "float3" 0.81481677 -7.4505806e-08 -4.260918e-08 ;
	setAttr ".tk[101]" -type "float3" 0.81481677 7.4505806e-08 -4.260918e-08 ;
	setAttr ".tk[102]" -type "float3" 0.77493697 -7.4505806e-08 -0.25179246 ;
	setAttr ".tk[103]" -type "float3" 0.77493697 7.4505806e-08 -0.25179246 ;
	setAttr ".tk[104]" -type "float3" 0.65920079 -7.4505806e-08 -0.47893769 ;
	setAttr ".tk[105]" -type "float3" 0.65920079 7.4505806e-08 -0.47893769 ;
	setAttr ".tk[106]" -type "float3" 0.47893757 -7.4505806e-08 -0.65920091 ;
	setAttr ".tk[107]" -type "float3" 0.47893757 7.4505806e-08 -0.65920091 ;
	setAttr ".tk[108]" -type "float3" 0.2517924 -7.4505806e-08 -0.77493721 ;
	setAttr ".tk[109]" -type "float3" 0.2517924 7.4505806e-08 -0.77493721 ;
	setAttr ".tk[110]" -type "float3" 0 -7.4505806e-08 -0.81481719 ;
	setAttr ".tk[111]" -type "float3" 0 7.4505806e-08 -0.81481719 ;
	setAttr ".tk[112]" -type "float3" -0.2517924 -7.4505806e-08 -0.77493721 ;
	setAttr ".tk[113]" -type "float3" -0.2517924 7.4505806e-08 -0.77493721 ;
	setAttr ".tk[114]" -type "float3" -0.47893757 -7.4505806e-08 -0.65920091 ;
	setAttr ".tk[115]" -type "float3" -0.47893757 7.4505806e-08 -0.65920091 ;
	setAttr ".tk[116]" -type "float3" -0.65920079 -7.4505806e-08 -0.47893757 ;
	setAttr ".tk[117]" -type "float3" -0.65920079 7.4505806e-08 -0.47893757 ;
	setAttr ".tk[118]" -type "float3" -0.77493697 -7.4505806e-08 -0.25179237 ;
	setAttr ".tk[119]" -type "float3" -0.77493697 7.4505806e-08 -0.25179237 ;
	setAttr ".tk[120]" -type "float3" -0.81481677 -7.4505806e-08 -4.260918e-08 ;
	setAttr ".tk[121]" -type "float3" -0.81481677 7.4505806e-08 -4.260918e-08 ;
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 2 ".st";
select -ne :renderGlobalsList1;
select -ne :defaultShaderList1;
	setAttr -s 2 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :initialShadingGroup;
	setAttr -s 3 ".dsm";
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultResolution;
	setAttr ".pa" 1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :hardwareRenderingGlobals;
	setAttr ".otfna" -type "stringArray" 22 "NURBS Curves" "NURBS Surfaces" "Polygons" "Subdiv Surface" "Particles" "Particle Instance" "Fluids" "Strokes" "Image Planes" "UI" "Lights" "Cameras" "Locators" "Joints" "IK Handles" "Deformers" "Motion Trails" "Components" "Hair Systems" "Follicles" "Misc. UI" "Ornaments"  ;
	setAttr ".otfva" -type "Int32Array" 22 0 1 1 1 1 1
		 1 1 1 0 0 0 0 0 0 0 0 0
		 0 0 0 0 ;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
connectAttr "deleteComponent13.og" "pCylinderShape1.i";
connectAttr ":sideShape.msg" "imagePlaneShape1.ltc";
connectAttr "deleteComponent15.og" "pCubeShape1.i";
connectAttr ":frontShape.msg" "imagePlaneShape2.ltc";
connectAttr "polyExtrudeFace15.out" "pCylinderShape2.i";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "polyCylinder1.out" "polyExtrudeFace1.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace1.mp";
connectAttr "polyTweak1.out" "polyExtrudeFace2.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace1.out" "polyTweak1.ip";
connectAttr "polyTweak2.out" "polyExtrudeFace4.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace4.mp";
connectAttr "polyExtrudeFace2.out" "polyTweak2.ip";
connectAttr "polyExtrudeFace4.out" "polyTweak3.ip";
connectAttr "polyTweak3.out" "deleteComponent1.ig";
connectAttr "polyTweak4.out" "polySplitRing1.ip";
connectAttr "pCylinderShape1.wm" "polySplitRing1.mp";
connectAttr "deleteComponent1.og" "polyTweak4.ip";
connectAttr "polySplitRing1.out" "polyExtrudeFace5.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace5.mp";
connectAttr "polyExtrudeFace5.out" "polyTweak5.ip";
connectAttr "polyTweak5.out" "deleteComponent2.ig";
connectAttr "deleteComponent2.og" "polyExtrudeEdge1.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge1.mp";
connectAttr "polyExtrudeEdge1.out" "polyTweak6.ip";
connectAttr "polyTweak6.out" "deleteComponent3.ig";
connectAttr "polyTweak7.out" "polyMergeVert1.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert1.mp";
connectAttr "deleteComponent3.og" "polyTweak7.ip";
connectAttr "polyMergeVert1.out" "polyMergeVert2.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert2.mp";
connectAttr "polyTweak8.out" "polySplitRing2.ip";
connectAttr "pCylinderShape1.wm" "polySplitRing2.mp";
connectAttr "polyMergeVert2.out" "polyTweak8.ip";
connectAttr "polySplitRing2.out" "deleteComponent4.ig";
connectAttr "deleteComponent4.og" "deleteComponent5.ig";
connectAttr "deleteComponent5.og" "polyExtrudeFace6.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace6.mp";
connectAttr "polyExtrudeFace6.out" "deleteComponent6.ig";
connectAttr "deleteComponent6.og" "polyTweak9.ip";
connectAttr "polyTweak9.out" "deleteComponent7.ig";
connectAttr "deleteComponent7.og" "deleteComponent8.ig";
connectAttr "polyTweak10.out" "polyMergeVert3.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert3.mp";
connectAttr "deleteComponent8.og" "polyTweak10.ip";
connectAttr "polyMergeVert3.out" "polyExtrudeEdge2.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge2.mp";
connectAttr "polyExtrudeEdge2.out" "polyMergeVert4.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert4.mp";
connectAttr "polyMergeVert4.out" "polyMergeVert5.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert5.mp";
connectAttr "polyMergeVert5.out" "polyExtrudeEdge3.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge3.mp";
connectAttr "polyTweak11.out" "polyMergeVert6.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert6.mp";
connectAttr "polyExtrudeEdge3.out" "polyTweak11.ip";
connectAttr "polyMergeVert6.out" "polyMergeVert7.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert7.mp";
connectAttr "polyMergeVert7.out" "deleteComponent9.ig";
connectAttr "deleteComponent9.og" "deleteComponent10.ig";
connectAttr "deleteComponent10.og" "polyExtrudeFace7.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace7.mp";
connectAttr "polyExtrudeFace7.out" "polyTweak12.ip";
connectAttr "polyTweak12.out" "deleteComponent11.ig";
connectAttr "deleteComponent11.og" "polyMergeVert8.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert8.mp";
connectAttr "polyMergeVert8.out" "polyMergeVert9.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert9.mp";
connectAttr "polyMergeVert9.out" "polyExtrudeEdge4.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeEdge4.mp";
connectAttr "polyTweak13.out" "polyMergeVert10.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert10.mp";
connectAttr "polyExtrudeEdge4.out" "polyTweak13.ip";
connectAttr "polyMergeVert10.out" "polyMergeVert11.ip";
connectAttr "pCylinderShape1.wm" "polyMergeVert11.mp";
connectAttr "polyMergeVert11.out" "deleteComponent12.ig";
connectAttr "deleteComponent12.og" "deleteComponent13.ig";
connectAttr "polyTweak14.out" "polyExtrudeFace8.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace8.mp";
connectAttr "polyCube1.out" "polyTweak14.ip";
connectAttr "polyTweak15.out" "polySplitRing3.ip";
connectAttr "pCubeShape1.wm" "polySplitRing3.mp";
connectAttr "polyExtrudeFace8.out" "polyTweak15.ip";
connectAttr "polySplitRing3.out" "deleteComponent14.ig";
connectAttr "polyTweak16.out" "polyExtrudeFace9.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace9.mp";
connectAttr "deleteComponent14.og" "polyTweak16.ip";
connectAttr "polyExtrudeFace9.out" "polyExtrudeFace10.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace10.mp";
connectAttr "polyTweak17.out" "polyExtrudeFace11.ip";
connectAttr "pCubeShape1.wm" "polyExtrudeFace11.mp";
connectAttr "polyExtrudeFace10.out" "polyTweak17.ip";
connectAttr "polyExtrudeFace11.out" "deleteComponent15.ig";
connectAttr "polyTweak18.out" "polyExtrudeFace12.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace12.mp";
connectAttr "polyCylinder2.out" "polyTweak18.ip";
connectAttr "polyTweak19.out" "polyExtrudeFace13.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace13.mp";
connectAttr "polyExtrudeFace12.out" "polyTweak19.ip";
connectAttr "polyTweak20.out" "polyExtrudeFace15.ip";
connectAttr "pCylinderShape2.wm" "polyExtrudeFace15.mp";
connectAttr "polyExtrudeFace13.out" "polyTweak20.ip";
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
connectAttr "pCylinderShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCubeShape1.iog" ":initialShadingGroup.dsm" -na;
connectAttr "pCylinderShape2.iog" ":initialShadingGroup.dsm" -na;
dataStructure -fmt "raw" -as "name=externalContentTable:string=node:string=key:string=upath:uint32=upathcrc:string=rpath:string=roles";
applyMetadata -fmt "raw" -v "channel\nname externalContentTable\nstream\nname v1.0\nindexType numeric\nstructure externalContentTable\n0\n\"|imagePlane1|imagePlaneShape1\" \"imageName\" \"/Users/MisCosas/Documents/LAD_ITESM/7mo semestre/Props/sourceimages/maxresdefault.jpg\" 2967037222 \"/Users/MisCosas/Documents/LAD_ITESM/7mo semestre/Props/sourceimages/maxresdefault.jpg\" \"sourceImages\"\n1\n\"|imagePlane2|imagePlaneShape2\" \"imageName\" \"/Users/MisCosas/Documents/ProjectKarts/Assets/Eduard/Armando Assets/hot-rod-v8-engine-3d-render-HA5Y24.jpg\" 1331715634 \"/Users/MisCosas/Documents/ProjectKarts/Assets/Eduard/Armando Assets/hot-rod-v8-engine-3d-render-HA5Y24.jpg\" \"sourceImages\"\nendStream\nendChannel\nendAssociations\n" 
		-scn;
// End of kart.ma
