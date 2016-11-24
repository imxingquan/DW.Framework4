/********************************
 * ºº×Ö×Ö·û´®×ª»»³ÉÆ´Òô
 */
using System;
using System.Data;
using System.Configuration;
using System.Text;

namespace DW.Framework.XConvert.ZH2PY
{
    /// <summary>
    /// Summary description for ConvertHzToPz_Gb2312
    /// </summary>
    public class HzToPy
    {
        private HzToPy()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region //gb2312ÖÐµÄºº×Ö±àÂë
        //01-09ÇøÎªÌØÊâ·ûºÅ¡£ 
        //16-55ÇøÎªÒ»¼¶ºº×Ö£¬°´Æ´ÒôÅÅÐò¡£ 
        //56-87ÇøÎª¶þ¼¶ºº×Ö£¬°´²¿Ê×/±Ê»­ÅÅÐò¡£
        // Ã¿¸öºº×Ö¼°·ûºÅÒÔÁ½¸ö×Ö½ÚÀ´±íÊ¾¡£µÚÒ»¸ö×Ö½Ú³ÆÎª¡°¸ßÎ»×Ö½Ú¡±£¬µÚ¶þ¸ö×Ö½Ú³ÆÎª¡°µÍÎ»×Ö½Ú¡±¡£
        //¡°¸ßÎ»×Ö½Ú¡±Ê¹ÓÃÁË0xA1-0xF7(°Ñ01-87ÇøµÄÇøºÅ¼ÓÉÏ0xA0)£¬¡°µÍÎ»×Ö½Ú¡±Ê¹ÓÃÁË0xA1-0xFE(°Ñ01-94¼ÓÉÏ0xA0)¡£
        //ÀýÈç¡°°¡¡±×ÖÔÚ´ó¶àÊý³ÌÐòÖÐ£¬»áÒÔ0xB0A1´¢´æ¡££¨ÓëÇøÎ»Âë¶Ô±È£º0xB0=0xA0+16,0xA1=0xA0+1£©¡£


        private static readonly string[][] _Allhz =
         new string[][]
   {
    new string[]{"A","°¡°¢ºÇß¹àÄëçï¹åH"},
    new string[]{"Ai","°®°«°¤°¥°­°©°¬°¦°§°ª°¯°£°¨´ôàÉæÈè¨êÓÞßíÁàÈïÍö°VÄË´ƒvƒŒƒùØÜ„’…¥ßÀ…Ù†‡Bàæ‡†ˆì‰a‰¹ÆæŠÖŠâ‹ÜÌÛ‘°‘¹”±”²•l•á™üšGš±œÜœâžGŸCŸs­a°}°Š²}³v´oµK½iËBÌ@ÖL×c×rÙŒÜtá{æXèPéuºÒêiêqëBì\ìaðgñLòIöJ÷oøÑÂ"},
    new string[]{"An","°´°²°µ°¶°³°¸°°°±°·³§¹ãâÖÞîáíï§èñÚÏðÆÛû÷öóƒ‡…\…{†H†††±ˆˆ¥ˆÝ‹F‹jŒå^¸É••›¡«q¯uºÐ±Q±V´UÁOÄWÇIÈCÈsÈ€ÉŽÑsÕYÖOØtØßVãQä@åBÇ¯éœêŽê›ë@ëˆì”íí™îOñüñKõcø‘ùgù“"},
    new string[]{"Ang","°º°¹°»Ñö…nŒì•n–‹áZálóa"},
    new string[]{"Ao","°À°¼°Á°Â°¾°Ã°½°¿°ÄÏùÞÖæÁâÚæñà»ÛêåÛñúòüéáöË÷¡÷éá®…†õàÞ‡Æ‡Ìˆ‰¥‰§ŠSŠW‹‹‹®CåŽS‘R’U’j“³“ý–À—`¹÷›|½½E²ÁŸÑ nª‡­H±l´x´“´ÂKÂOÆbÊTÎ‚Ò\Ö’Ö“ÝEàUçGéOëJòˆö—ø^ø€úqü"},
    new string[]{"Ba","°Ñ°Ë°É°Ö°Î°Õ°Ï°Í°Å°Ç°Ó°Ô°È°Ð°Ê°Ì°Ò°ÆôÎÜØá±öÑîÙ÷ÉÝÃå±”²®…©†\†^ˆzˆ¢‰‰ÎŠBŠ‚Qy’i’pÞã–[èË–Â™ñÅÈžß ã«X°j°q³F¸Ÿ¼“ÁTÁjÃ_ÆžÝÉÍMÒ†ÔyØ^Ú•ÝRá—ášâZïTôƒõEõN÷„÷ˆü–"},
    new string[]{"Bai","°Ù°×°Ú°Ü°Ø°Ý°Û²®°ÞÞãßÂêþ†hŽß°Ç’…’“ÅÅ”[”¡–àÅÉªW¸q»“»Ÿ½]ÞµËbÒoÙ”ì‹÷¹ív"},
    new string[]{"Ban","°ë°ì°à°ã°è°á°æ°ß°å°é°â°ç°ê°ä°íñ­ÛàîÓô²Úæñ£K·ÖˆmˆÐŠ”Œê±òE“„”‘”Ê•L–D–®œ°­š¶t»O»{½OÃRÎZÎ†ÎŒÑ—ÒƒáÙÛAÞkÞl±æ±çÞnÞqâkã[é›ì‡îCô‘øX"},
    new string[]{"Bang","°ï°ô°ó°õ°÷°î°ñ°ö°ø°ð°ò°ùäºÝòK†çˆ ˆÈ‰Y‹˜LŽ°ŽÀŽÍÅíÏ’²’Ê“sÅÔ—” ¥«g³‰¶œ¼½‰¿R·ÄÅÍKÍ{ÎMó¦Örß™æ^íDòuóo"},
    new string[]{"Bao","°ü±§±¨±¥±£±©±¡±¦±¬°þ±ªÅÙ±¢°ý±¤°ú°û±«ÅÚÆÙöµæßìÒñÙð±õÀÝáÜƒ˜„ƒÙè„ô´ô‡E‡¥ˆçˆó‹~‹›Œ‡Œ—ŒšÞA•Þ–¢«’³h·‘¸²¾¾‹Ç˜Ê}ËÌ™ÍdÐˆÅÛÙöÑfÒJÙ…ãEètè˜é–ìdìsï’ï–ñhóbóŽõUøRødý_å²"},
    new string[]{"Be","È`"},
    new string[]{"Bei","±»±±±¶±­±³±¯±¸±®±°±´±²±µ±º±·±¹±ÛñØã£ÝíðÇöÍßÂÚý÷¹ØÃÚéíÕ‚pÙÂ‚³‚Ë‚äƒF†\†h†Õˆ¢ÛýâöÊ‘v“d•K–{–È—G—f—“—”—À²¨ ´ ÍªN¬D¬i¯w° ¶F¹t¼LÆpÆ…ÆÐÝÉÈiÆÏËÍ“òãÒoÕRÕ|Ø°ÏÝKÝ…àfãmä^åCèEócùl"},
    new string[]{"Ben","±¾±¼±½±¿º»ï¼êÚÛÎÛÐÌå‚–†Ï‰úŠM’Ù“à—L—ñ›yœ`žÇŸø ÄªŠÁÏnÙSÝ™ßGåQèM"},
    new string[]{"Beng","±Ä±Á±Â±À±Å°ö±Ãê´àÔÈÙº°ø‚õßô†çˆ©ˆÈÜ¡‰lŠRÐÆ½Åê’²“sÅÔ°ñmŸÔ¬a¬e¯nµp½l¾X¿‡ÈEÛMßJåAçaéGéaìž"},
    new string[]{"Bi","±È±Ê±Õ±Ç±Ì±Ø±Ü±Æ±Ï±Û±Ë±É±Ú±Í±Ò±×±Ù±Î±Ð±Ó±Ö±Ý±Ñ±ÔÃØÃÚïõÞµÝ©ÜÅÝÉØ°ñÔî¯ÙÂæÔáùóÙóëó÷ô°ÜêôÅâØîéõÏßÁã¹êÚääå¨èµßÙ÷Âåöåþæ¾ØòÓØ·ð‚¿„ö±°…ñ†ž†ôˆfÛýˆã¸´‰ýŠ`ŠŒŠËæÇ‹ïŒÂš·ùŽÅŽÆâÏYŒ’PWÏ·÷Þã”À”è·þ–aèÁ–Š–©–Ä—a—À—ééÞéèšÈ›a²¨œ œü§Ÿ•ŸÎª‹ªŒ«®n®w®…¯H¯R¯w°n°zÆ³µ–·K¸“¹P¹t¹u¹vº`»z»¼„¼ž¾a¿oÀVç¢ÁTÁXÁ‘ÂÃYÃZÃ^Æ¢ÄMÄbÆƒÈ]ÉœÌYÂÇÍšÎ“Ð‹±»ÒKÒgÓvÔv×ØPØ„ÙCÙMÙSÚF·ÑÚPõËÛ~Û‹ÜKÜLß›àˆàŠâtãGåCæqç@èEöÍé[é\é]ésê\ÚéÚðì‹í@íSí{ïð{ñEð¥ñƒòóôxô“õIõmös÷”øpùSù›úzú‡ûGü„"},
    new string[]{"Bia","÷Ô"},
    new string[]{"Bian","±ß±ä±ã±é±à±ç±â±á±Þ±å±æ±èâíí¾ØÒãêíÜòùñÛöýóÖÜÐñ¹ÛÍçÂìÔ‰ä·âOÌÆ’\ÞÕ“O•c›Mž× ¤ªpª ®K¯V·Hð¡¹»e¼D¾Ž¾œÅXÅŒÈqËxÒŒÓS×ƒØPÙHÞgÞkÞlÞpÞqß„ß…ß›áŠæQérì™îYöböcøuú@÷Ô"},
    new string[]{"Biao","±í±ê±ë±ìè¼æ»ì©ì­÷§ñ¦ì®ïðñÑæôïÚ‚lƒGƒšØâ‰wæÎŽ¼Ò“¿˜Ë™~œWœýÆ¯ždìáŸÏ gªY·…ºgÃ ÄrÅA°úÊEË‘ÒFÕ•Ö€Ù™ål÷éçSèsïRï[ïjïkïlïnòŠóQóT÷Bû÷Ô"},
    new string[]{"Bie","±ð±ï±î±ñõ¿„e…ñ•Ö°Ç°Î°Æ“ÅÆ²–Â–Äªm°TÃØ·ÆƒÇa±ÎÌ‹ÍrÏhÒX÷Mü‚ý–"},
    new string[]{"Bin","±ö±ô±÷±ò±ó±õáÙë÷éëçÍ÷ÆÙÏéÄ÷ÞïÙçã·Ýƒ†”P—Ãš›šàšñäºžIžMžl¬ž­p³WÀ_ÄœÌžÏ™ÓŸØhÙeÙfÙšÚSß“è\ìEîlî Æµóxó‰óôW"},
    new string[]{"Bing","²¢²¡±ø±ù±û±ýÆÁ±ü±ú±þÞðéÄÙ÷ÚûK•ã‚v‚§‚ìÙûˆ—Œ}Æ½ŽÕŽðT’mÆ´’ò•\•mèÊ–Þ–â—€—Š™‰šê ]ìÞ¬V¯n°R°S±}·A·’¸p½l½Žç®ÆuÍsÕ@ÛMâãuä‰êvìhìí@íSïžðVõmðÚ"},
    new string[]{"Bo","²¦²¨²¥²´²©²®²µ²£°þ±¡²ª²¤²§²«²±²¯°ã°Ø²°²³²¬²­²²ÆÇ²·íçõËéÞÙñð¾õÛà£Þ¬ô¤îàâÄë¢·ð‚Nƒ`ƒk„ƒÄ¼†\‡h‡¥‰®Š‚ØÃŒXóŽ“°ÅÂö‘ÅÅÄ°Î’©“Üß¨±©·þ–Â—K˜_™q™Øš†ÆÃ›Âœ_œ”ŠÅËÆÙŸ¹±¬ ¦ Ý é þªt­“­”·¬°h°l°×°Ù°±C³j´B´‘µR¶z·q¸Ÿ¹º~²¾¼\¼žÀÃJÃ`Å‡åõÆtÆ…ÆžÆÐÈ•ÆÑÊNÊXÞµÞÁÌYÍoÐ“ÑBÑJÒTÒUÒqÔy×LØmõÀÅÜÜ@àRâ“ãKã\äcænè}éDðGðoñAñCñFñgñ•ò’ómópõEõN÷QöÑ÷ˆùPêþ"},
    new string[]{"Bu","²»²½²¹²¼²¿²¶²·²¾²¸±¤²º²ÀÆÒê³åÍêÎîßîÐõ³ß²ÑƒW„Ï…Ä…ùˆ¶ŠçŒ mŽïE‹’pÞÔ’Ã’Ñ“ä“ò–¿šhšiäßªŽ¶¹rº^Ç[Éž±¡ÑaÕcÛYÝ•ÞKà^âbâ˜¸½ê†÷¹ðJðXõ‹øGùLûQ"},
    new string[]{"Ca","²Á²ðíåàê‚ð‡Í”c™U´~µg²Ìßn"},
    new string[]{"Cai","²Å²Ë²É²Ä²Æ²Ã²Â²È²Ç²Ì²Ê‚š‚Æ†’ˆÆŠéŒu‘å’A’ñ“H—¾Z¿nÀuØ”ÛP"},
    new string[]{"Can","²Ï²Ð²ô²Î²Ò²Ñ²Í²Óæîè²åî÷õôÓ‚ðƒ……¢…£…¤†Ð‡A‡k‡Ô‹Û‹ìß‘K‘L‘M‘”‘â“·•üšˆœ\œ’Ó N |·_ºdËLÎ]ÐQÐTÖÛŠçDï{ïŠò‰öYöŸ÷“üo"},
    new string[]{"Cang","²Ø²Ö²×²Õ²ÔØ÷¨‚}‚áƒû…MÈ™âœæžPª¬šº[À˜ê°Å“ÉnÊiÏ@Ù‰è†úIû]"},
    new string[]{"Cao","²Ý²Ù²Ü²Û²ÚàÐô½ó©äî‚óåøæóý‘F‘¨“Ù•ùÔèÃHÆHÜ³É˜ÒGÒ_Ôìà“ç[èAòxü"},
    new string[]{"Ce","²á²à²ß²â²Þâü‚ÈƒÔ…‹‰x‹¨àýŽ¾ŽúÅ‘Š’‘”˜–ÅÕ¤œy®‚¸ž¹Z¹k¹‹ºu»ÇRÈYÈmÉƒÉâývØÖ"},
    new string[]{"Cen","²Îá¯ä¹…¢…£…¤ß—q›N³•·_¸’ºdÄ~"},
    new string[]{"Ceng","Ôø²ã²äàáÉ®ÔöŒÓò™I¸}¿•çÕòš"},
    new string[]{"Ceok","³€³’"},
    new string[]{"Ceom","K"},
    new string[]{"Ceon","ªe"},
    new string[]{"Ceor","u"},
    new string[]{"Cha","²é²å²æ²è²î²í²ë²ì²ç²êÉ²²ïé«é¶ïïñÃãââÇéßæ±è¾ïÊàêš÷‚²„x†âˆ“ŠgŒð¿’K’Q’·’¼½Ý½Ó“c“ Ð±–Ë—^âª®›¶g¼pÃPÅaÅ‘ÆOÜÚÇNÝ±Ñ–ÓÔˆÔŒÛ‚âOã˜åšæ\èdîÎìxðlÔû"},
    new string[]{"Chai","²ñ²ð²î²òîÎðûò²Ù­ƒŠ„Ð²æ†¶‡Ð’K´ê²é åµ}ÜëÆÊOÏŠÐƒÓâOýbö·"},
    new string[]{"Chan","²ú²ø²ô²ó²û²ü²ù²÷²õµ¥²öêèæ¿ÝÛÚÆÙæâÜåîó¸åñïââãäýìøæöõðå¤P×ƒ]ƒdƒ{ƒ§ƒ·ÍÃ„i„}„•„­…gÀå†®†Î‡c‡Á‡ÏˆF‰‰Ê‹ÈæÓÕ¸ÝãäŽfŽÂŽÊ‘„‘Ï‘Ôµ§“˜“·“½“Û”v”â•C—{—˜^™Ùš´Õ´›º½¥œµIu¨žežž¬ŸžŸíª†®a®b³ƒ´v¶Uºo¾g¿CÀAÀWÀpÀsÕÍÃˆÆBÉ»ÊrÏMÏsÏ€ÐŸñÏÑgÒRÒbÒcÒ—ÕSÕ~Ö×€×‹×ÚßÛ…Þ{àšàžápã@äaäiçPèéKéˆêUí]îð’"},
    new string[]{"Chang","³¤³ª³£³¡³§³¢³¦³©²ý³¨³«³¥²þÉÑöðë©ÝÅã®æÏáäÛËãÑâêØöêÆÜÉæ½Ÿ‚tÌÈƒYƒ”ƒ¯ƒ¸…”‡L‡Ÿˆö‰jŒ¬ÉÐS•˜•³—–—ÇÌÊœCŸ…«`¬d¬„¬ ®D®^®˜Ã›ÄcÄqÈOÏ^ÑmÕkää–å_çLè éLéMé‹êOíoöK÷l÷•ü"},
    new string[]{"Chao","³¯³­³¬³³³±³²³´³°½Ë´Â³®â÷ìÌñéêËž£„¤„àßë‡ZŽlŽz€“¼˜©˜È™ùÌÎRýŸqŸ· Ÿ±|¸J»}½B¾K¾b¿U¿žç§ÉÜÁVÓeÔNÖaÖšÖßÚ}ÚˆÞCà}ânûžü{ü…"},
    new string[]{"Che","³µ³·³¶³¸³¹³ß³ºÛåíº¼‚e‚®„ï…ã†q†Ã¶à‰ïŠbÕ¬åøØ“F“µ“Ý³â³ØŸEŸLŸc …²u³Œ³ÂsÇpÍ’ÔaÖÜ‡ÞŠîJ"},
    new string[]{"Chen","³Ã³Æ³½³¼³¾³¿³Á³Â³Ä³È³À³»é´ÞÓÚÈí×Úßå·ö³àÁØ÷è¡‚E‚áƒ¡‡¸¿°‰\Ìî‰m‰}‰öÁ±×’×“Z”•æÕí—F—£—²˜¹™ÂÉòÕ¿žcŸGŸ‹¯MÕî¯„¯’íñ²_³•´~·Q¾D¿bçÇëÀëÏÆÇ_ÇkÊcËlÏIÒrÔHÕ€ÖRÖnÖ×ÙoÙ•ÚfÚ’Ú™Û{Ü•Þápâ\åŒêJ´³êëúmû‰ýYýZ³Ó"},
    new string[]{"Cheng","³É³ËÊ¢³Å³Æ³Ç³Ì³Ê³Ï³Í³Ñ³Ò³Î³È³ÐëóèßÛôîñàáîõõ¨êÉñÎèÇòÉØ©îª\Øö‚D‚t‚ ƒ\¾»Çº†ÜˆÁˆá‰SŠ¿ŒkwáÓŽñ»Õáç‘‘r‘~‘ÍÇÀ’¬’Þ“Œ“£“¬“Î“Õ³¨–b—–—¢—¼˜ŒéÌ˜û˜ü™f™ršé›„›“ä¥›Æ›ÕœQœË¯žjžs  ª«ž¬A¬b¬š®—¶¢±²…³·Q··œ¸V¹f½†¿BÃwÃ”Ç^ÍBÏ|ÓcÕ\ÚWÚXÌËÛk±ÄàJÛ«ÐÑä…æjçdçpèKêpìlîdð‰òGòrõ“üh³Ó"},
    new string[]{"Chi","³Ô³ß³Ù³Ø³á³Õ³à³Ý³Ü³Ö³â³Þ³Ú³Û³ã³×õØÛæÜÝÜ¯âÁæÊôùñÝë·ßêñ¡ò¿à´ð·í÷ó¤óø÷Îß³áÜó×àÍÙÑÌø‚s„„„È…h…q…µ…Õ…ä…æ…ê¶ßÐ¥Ï²†Ë‡V‡[‡i‡„ˆkˆ‰ŠLËýŠwŒÑI«¯Ópu‘J‘d‘y‘´’LÌ§’x²ð’„ÍÏ’’»ÌáÞõ“¤“¹”~–o–«èÜ˜»šIš^šlšnšýãû›nÖÎ›‚œFœ‰ÖÍœþkžÃŸUŸë ô®E¯b¯v¯€°Víô²lµoÀëÒÆ¸‡¹M¹x¹}»Œ¼Y½‚Á‹Â@ÂBÂ]ÃLëÕÃnÃqÃ’ÄSÜÎ²çÇKÀòÇ ÍNÍhÉßÎyÑDÑEÑlÔWÔ ÕBÕvÖsÖ–ÙPÚdÚmÚpÚ†Ú—ÛFÛLõ½ÛyÝBÞ‹ÞŒßWßgßoßtß†ãMãrã‰å~ëxë†ï†ï—ðSð„ÊÎñYòóPæêøTø|ùAù`ùù•ù—úuüJü[ýXýcÛ­"},
    new string[]{"Chong","³åÖØ³æ³ä³è³çÓ¿ÖÖô¾âçô©ï¥ã¿Üû‚£‚òÙ×†Á†üˆÃŒ™ƒ×‘o“_“›˜¶›_›ÒräüÖò ‚«–¯\ÖÑµr·N¾…ÁZÁˆ¼ëÎuÏxÐnÑ~ÛŒÛ Íªã|ê™"},
    new string[]{"Chou","³é³î³ô³ð³ó³í³ñ³ê³ï³ì³ë³òã°Ù±àüñ¬öÅE‚G‚¸ƒ‰…Á‡œÛÚæ¨‹B‹‹áŽÎJ‘ÀÅ¤’ôÞí“o”F–ƒ–„–ä—¹™„šŽäå b ¶ ß â®‡® °{±T±y²ƒºN»I¼—½[¾IÅWÇ“ËgÑnÔ—Öa×p×‡×‰Öß×žÛSÜPßcáOábáhá~âoÅ¥ëlô{õ\õöÖ"},
    new string[]{"Chu","³ö´¦³õ³ú³ý´¥³÷³þ´¡´¢Ðó³ü´£´¤³ù³ø³ûèúèÆÛ»âðç©Ø¡ãÀõé÷íòÜéË‚m‚âƒƒ¦„IÖú‡bˆÇ‹ƒŒçŽÐN‘A‘Ã’}“ª“¹”™”ßÄû—Æ˜Z˜™[™s™Ÿ™¬™»™úšbÍ¿Êçœäéžã Ë¬G¬`­lÁòµA×£µ—¸a¸eºX½IÐõÂ^ÂaÄ•ÆcÇˆÖøÉZÉeÉÊxË ÌŽÏ{ÐEñÒÓcÓ|ÔxÕ‘ÖTÚ°ÖîØXØaØŒÚnÛHÛUÛuÜXàsãIäzézërúRúžýiýsýƒåø"},
    new string[]{"Chua","´éšHš_"},
    new string[]{"Chuai","´§ëúà¨àÜÞõõßšIÄDÄu"},
    new string[]{"Chuan","´©´¬´«´®´¨´­´ªë°å×îËô­â¶çÝ‚÷ƒb„”‡ù‰@ã·Þò•ÄšNšöªk«[¬®Uº@ÄxÅxÇFÙiÛwõßÝŽâAïéúE"},
    new string[]{"Chuang","´°´²´³´´´¯´±âë²Ö¨‚}‚ü„V„k„y„€„“‡l‡è´Ñí‘ê“œ–S™Hrw — ¡ §¯´}·™¸RÄ€ô©´ÐÊ[êJ"},
    new string[]{"Chui","´µ´¹´¶´¸´·×µé³é¢Úï‚…‡ùˆ§·“€–û¹ŠÄDÇ”à]åNæmîqôDôsý—"},
    new string[]{"Chun","´º´½´¿´À´¼´¾´»òíÝ»ðÈ‚¤ƒb‰@‹aÃ•I•«ëÔ–~˜J˜‡˜ê™šãç›Ìœ·œ÷_ Æ¬t²Q¹—¼ƒëÆÃaÃ‹ÄxÆXÈNÈoÉOÉ”ÙƒÛwÝbÝéúácåTêõžöjù‡ùœ"},
    new string[]{"Chuo","´Á´ÂõÖà¨öºê¡´Ù…É‡ÇŠÅŠÆ‹C‹SŒF·’‘“óí½šf›íÄ×ìÌ´‡¹–´Ø¾Y¾b¿ž×ºÄJÜõÝýÚ}õÀÛTõâ³ùÝzÞuåÁßOáQÈ©äråYæ—èqïßýpýw"},
    new string[]{"Ci","´Î´Ë´Ê´É´È´Æ´Å´Ç´Ì´ÄËÅ´Ã´Í²î×ÈßÚðËìôôÙÕè‚½„p²Þ…‹Ë¾…è†ˆˆˆôŠœ‹ãáÏÕŽãŽú´ë–c–Ÿ–²²ñ–Ü–æ›×ÌÐžB«u«y®N°rµQôÒ½a¿WÃhÜëÆ˜ÆÜùÇ„ÈWËFËjòºÍyó£ÎˆÏ…Ô~ÙnÚaÚeôôÚÞeÞiÞoâ‘ï“ð@ódóqõJøyú\ú]ý€"},
    new string[]{"Cis","†ï"},
    new string[]{"Cong","´Ó´Ô´Ð´Ò´Ï´ÑçýèÈäÈè®æõÜÊÙÌ…²‡èŠæŒQ¾ÀÄòS•›‘F‘m•¾—Œ˜B˜º˜Ú˜â™ßY^|ƒœžšŸtŸÐ Q ­B²j³Ÿ´°ºb¾t¾‘¿k¿v¿‚ÀS×ÝÂ‡ÂŒÂ”Æ‰ÇˆÉÊ[ËqÏZÕpÖÙzÙ{çWò^ò‹"},
    new string[]{"Cou","´Õé¨ê£ëí×à×á×åœ«u´ØÝýÞ´Ë’Ç÷È¤Ú…Ý"},
    new string[]{"Cu","´Ö´×´Ø´Ù×äáÞâ§Ýýõ¾õ¡éãõíÇÒ…a‡mŠÅŠÆ‹{I‘–ÆÝ’Û¯|¯•°š¿U¿qç§ÃÊIÊPÓcÕKÇ÷ÚuÈ¤Ú‚Ú…ÛUÛcÛnÛqÜAåe´íî•û€û‚û„û›üy"},
    new string[]{"Cuan","´Ü´Ú´ÛÔÜÙàìàïéß¥ƒVŽm”e”x”€ê¿™«™çš–žUž£Ÿä·‰¸U¸ZºeºxÇˆÒ{Üfägè‰"},
    new string[]{"Cui","´ß´à´Ý´ä´Þ´ãË¥´á´âè­ßýã²ÝÍë¥éÁtºÌå‚yƒþ†Ÿ‰…²ìŒœéõ‘N—½˜§yûŸnŸÕª‰¬X¯Q°„´…¸W»‚¼¾\¿\¿…ÀŠÁŒÃyÃœÄƒÄ‹Ä›ÒPÚ~ÛnçJö¿îx"},
    new string[]{"Cun","´å´ç´æ¶×ââñåü„Y…¼‰–’Ž›–¿£´¸€»vÛZß—"},
    new string[]{"Cuo","´í´é´ê´ì´ë´èáÏØÈõºëâðîõãðûï±ïó„v„z‰èÕŽó´ÝÎô×î— Ì I¬›±‘¿WÇsÇuÉcÉxÊPÌ‘ÒPÕ‹õòÜgßHßuàŸáAáiäSåeóqûzý€"},
    new string[]{"Da","´ó´ð´ï´ò´î´ñËþóÎÞÇßÕñ×ðãâò÷°æ§í³àª÷²‡„‘…A…ì…ö‡}ˆ™Ëú‰¡‘„“‚“Ò™\šÎšùœÍžØ [®}®†±o³K¸—ÀJÁeµ¨ÇEÇQËRÏƒÓuÔzÛQÜJÞ…Þ‡µüßQß_æ]æpèNí^ý‘ý“"},
    new string[]{"Dai","´ø´ú´ô´÷´ý´ü´þ´õ´ûµ¡´ö´ó´ùß¾çéåÊá·ß°÷ìææçªÜ¤þ…¦‡Nˆ‚Ž‘Ž¡Ž§K‘·•Î–±é¦¶¾šùžŽªy¬x¹yº‰½H¿DÅ•ÊOÍfÎ}ÏEÒyÔrÚ±ÙJÛFÛÜÜ–ÝDÞaåÖßfßrÁ¥ìOì^ñWñjñ~ÍÔõ\ølün"},
    new string[]{"Dan","µ«µ¥µ°µ£µ¯µ§µ¨µ­µ¤µ¢µ©µªµ®µ¦µ¬Ê¯ðãå£ð÷ÝÌééíññõóìêæÙÙà¢S·‚„ƒdƒ{ƒÑÈ½„[„é…S…g…ì†m†›†²†Î‡d‡n‡~‡·Ì³‰¯ŠlŠ½‹[ŽŽ—³Àâò´×‘„‘žº¶’b“Ú“Û“ú–½éÜšKš—›X›Õ¿Ì¶å¤ÚŸí ý«m­®X¯D°D°Q°œ³N¶V·žº„¼ÀWÂnÂ›ÄEëþÄ‘ÍžÑÑÏ€ÐyÑÒRÒbÒ—êèÓgÓ”Õ²ÕQÖÙœÙ ÉÄÛÜlàáGá]ìKîFðZð…ñdñšó‡ø}ü^ülürÚàØé"},
    new string[]{"Dang","µ±µ³µ²µµµ´ÚÔîõå´ÝÐÛÊñÉí¸‚«ƒ}‡ŽˆW³¡ˆ›ˆö‰³‹P¤ÉÕ“õ”†™n™éšë‹ÇžªÌÌ C«š¬„­T­c®G®”¯ƒ±U²^´XµD¹Yº‚ºšÅ™ÊŽÌoÏ}Òd×[×•ÚßTèKêWë‹üh"},
    new string[]{"Dao","µ½µÀµ¹µ¶µºµÁµ¾µ·µ¿µ¼µ¸µ»àüôîâáìâë®ß¶Ù±ƒ‰ÊÜßú†ý‡‹‰»ŒpŒ§ŒàuëìŽWŽÎìýã°’Ò“v”F–]—Í˜˜™|™„ä¬ÌÎý c­±Iµ”¶\·R¹|½rÁŸÂRÅsÈKËgÍ@ÐmÐpÑnÜ„á~á’ê‰ÌÕëIëZô€÷øBÄñØÖ"},
    new string[]{"De","µÄµÃµÂµ×ï½‡NÔzœ¿—‘›úµÇåuÚì"},
    new string[]{"Dei","µÃ†O"},
    new string[]{"Dem","“g"},
    new string[]{"Den","’O’Y"},
    new string[]{"Deng","µÈµÆµËµÇ³ÎµÉµÊµÅíãïëàâáØê­ô£ƒ\‰œ‹¿‘~³È™žŸô­O¸~Å˜ÓRØOà‡ç‹ëQ"},
    new string[]{"Di","µØµÚµ×µÍµÐµÖµÎµÛµÝµÕµÜµÞµÌµÄµÓÌáµÑµÏµÒµÔµÙêëÛ¡ÚÐÚ®àÖèÜ÷¾ôÆØµé¦íûæ·Ý¶íÚïáÛæÙáíÆª‚d‚±ƒCƒ™É×…}…à†v††¬à´‡”ˆhˆkˆªˆ¯ˆ¹‰y‰„‰—ŠD‹XÞŽRftw~µK‘d’F’†’ã“W“Ÿ”³•Aè¼–m–š—\—b˜N˜µ›ÁœvœìŸb ¹«Z«Ÿ®S¯F±ƒ´Y´”µ¶Eºa¼e¼s¾†Ô¼Â‚ÃJëÕÄVÉÖÆlÆmÝ¯Ç…ÇœÉ‰ÊHÊLÊOËyË‹ÍhÎ[ÏEÐ”Ó]ÓhÔgÕœÖBØpÚdÚhÛqÛyÌãÛ‡Û—ÝBÖðÞž´þßfßmßrâKãdå~çCêsêÁ¥ì{íLîEî}ÌâñVóƒóžô†öWûM"},
    new string[]{"Dia","àÇ"},
    new string[]{"Dian","µãµçµêµîµíµàµßµæµâµëµìµäµèµåµáµéõÚîäÛãÚçñ²ô¡çèáÛñ°×‚Ù…Ž†ˆÛþ‰|‰«ŠHŠû‹LÑŽoŽp‘úÄé”„”“”¥—Ï˜ˆ˜•˜ë™AÕ´ÏÑœ¶Õ¬U¯t¯’°d´ÄHÉ_ÊsòÑÍŸÔaÛ†âšëŠîFîŒîò›ücý‚Ø¼"},
    new string[]{"Diao","µôµöµðµõµñµ÷µóµïµòÄñîöï¢öôõõ®Ù¬ÙÃµ¶„aŠP‹àŒÅt‡¬’FÌô—¹š“šôœ@¬h¯š²f³H³í·–¸L¸uºyôÐ¼g½r¾I³ñÝ¯É‰ËyÍ@ÍqòèÓŽÕAÕ{ÕÔÚwÌøõÖÝUé÷ážâyäHä”åcèSëïMæôô†õMõ øBøJù@ùmûbü—"},
    new string[]{"Die","µùµøµþµúµûµüµýëºÜ¦ð¬ÞéõÞñóöøÛìà©Øý†A†O†—U«ÞŽ²LgÂ‘ä’”’¡•i•è–»˜G˜›šŠšÛ›uÉæœhäÍ š®’®¯A¯B±y±‚ÖÏ½xÀ„ÂWÃ]ÖÁÅ\ÅŽÆ|ÎHÏHÑñÞÒBÔeÕ™ÚgÛ@ÛLÌßÛÝWéóç“èFéPíCõ]ölö÷£õÚ"},
    new string[]{"Dim","‡Ã"},
    new string[]{"Ding","¶¥¶¨¶¢¶©¶£¶¡¶¤¶¦¶§î®çàîúëëíÖðÛØêñôôúà¤µìŠcàŽŠâ’ð—ÅÍ¡ìµÆ®k³G´O´ÂˆÆJÈbÝãËYÍBÓ†á”äbåVç–ìwí”îrï}ð—"},
    new string[]{"Diu","¶ªîûGäAïM"},
    new string[]{"Dong","¶¯¶«¶®¶´¶³¶¬¶­¶°¶±¶²á¼ð´ÛíëËëØíÏë±á´ßË‚”ƒPƒö„Ó„çˆÄ‰’ŠŸŠà‹Ùd–ž‘ã’œ“_•k–|Í©—šæ›òœ§žúŸüªJð®¸•Í²¹c¹š½pÄLÆ{Ç‡ÊÎXÐhÔ˜Õ‰Þ“ÍªëšñŽòLõ[öCù…úHüŠâº"},
    new string[]{"Dou","¶¼¶·¶¹¶º¶¸¶¶¶»¶µ¶Áò½ñ¼óûÝúc‚JƒÃ„E„r…Ê†tÍ¶”Ô–’—u™XšÃšÑ›ÃäÂž^²fñ¾¸]Ã–ÅÇW×xÓâàKáHõ¢â^äWî×ékêLêhðLðôYôZô^ô`ôa"},
    new string[]{"Du","¶Á¶È¶¾¶É¶Â¶À¶Ç¶Æ¶Ä¶Ã¶Å¶½¶¼¶¿¶Ê¶Ùó¼óÆà½äÂèüë¹÷ò÷ÇÜ¶ƒ™„E„†„‹…X… ‡€‰TŠ‹óÕ¬Žª”¾•’•¤˜Ì˜ÐéÒ™³š˜šœ›èž^ © Ùªš¬o­{°²G¶Š¸]óÃ¸‰ºVôîÇTÎ}Î–ÐCÑtÒeÒlÓGÔŒÕi×x×˜²ïØKÙ€ÚGá`åLåƒæNèoÕàêAê^ê•ì|íbí~íîDòyüt"},
    new string[]{"Duan","¶Î¶Ì¶Ï¶Ë¶Í¶Ðé²ìÑóý‚Ç„Œ‰F‹eåè”àš¬¬‡´Vº@»f¾„ÂZÄaÈ˜ÑƒõßÜYå‘æH"},
    new string[]{"Dui","¶Ô¶Ó¶Ñ¶Ò¶ØïæíÔí¡í­ƒµƒ¶ˆŒˆÍ‰[¶áŠZŒŒ¦Å‘‡‘»“€–€žAžSžwž}¯y´qµq½˜Ä„ËcÖd×B×m×·âqäJä„åTæmç…çŽÈñêŒê îXø‹"},
    new string[]{"Dul","c"},
    new string[]{"Dun","¶Ö¶Ù¶×¶Õ¶Ø¶Û¶Ü¶Ú¶Ý²»õ»ãçíïïæíâìÀí»¯¿¡‡‰•‰ÝŽÝ÷ª‘‡“Ç“æ˜J˜ú—Ÿõ Ôª–´]ÄRÄ]ÎPëàÛvÜHÜOÞšßqâgç…çŽîDò—"},
    new string[]{"Duo","¶à¶ä¶á¶æ¶ç¶â¶å¶è¶é¶Þ¶ßÍÔ¶È¶ãõâãõßÍîìñÖßáç¶šƒµƒ¶¶Ò„A„m„„„‹…¼†Æ‡š‡¾ˆ‘ˆÊ‰™‰š‰ïŠZŠb‹s‹µŒ¹“ü‘†’–’—´·´§”Ÿ”£”¦”­–\ÔÓ–m–šèÞ–ª–Ã–úé¢—Ù™EšÇ›kãûÉ¯k³›¾EÆ–ÑEÔqÕBØyÚrÛFÛGÛTÜoÜ€µ¦àâ‡åTæNèIÕàÍÓêwêyËåëDï˜ð™ñWñjôDõyùzüc"},
    new string[]{"E","¶öÅ¶¶î¶ì¶ê¶ó¶í¶ï°¢¶ô¶ë¶ð¶ñ¶ò¶õï°ÚÌÛÑïÉãÕÝàÜÃéîæ¹Ý­öùò¦ëñãµßÀØ¬ðÊåíÑÇ„†Î±àÙ¨‚­‚Îƒ^ƒi„þ…\…v…Å…Ù†@†HÑÆ†s†‘°¡ßý†¡‡f‡Ù‡êÛëˆºˆ×ˆìˆñŠŠŠŽŠ´ŠâŠã‹jŒßŒïSk¬âÖŽþ™º‘ö“t“~“”AêÂ–•—¿™ÄšGšd´õšx›¡›áœŠ«M«¬c¯u°x±“³S³X³b³j³ríÒ´dµJ°·ÉJÊ‚ÌFÍLÎYÓFÓžÔ›ÕMÖ@×F×†Ø`Ü—ÝQÝ‘Þˆß]ß{ÒØâeä~åŠèyépé‘êiêq°¯îOîPî~î€ðIð_òFØªô‰ôŠötö÷{ø‘ùEùZù[ù˜ýLý|ý…"},
    new string[]{"En","¶÷ÞôÝìàÅŠCWŸ¸ð†ßí"},
    new string[]{"Eng","íE"},
    new string[]{"Eo","˜"},
    new string[]{"Eol","s"},
    new string[]{"Eom","™ë"},
    new string[]{"Eos","”ñ"},
    new string[]{"Er","¶ø¶þ¶ú¶ù¶ü¶û·¡¶ýçíöÜð¹Ù¦åÇîïõƒ¹ƒº„n…þ†„‹èŒ©ŒªXpr–k–é–ê˜Þš¾›˜œxå¦ –»•ÂYÂxÃsÄžÇHËnÐ^ÑLÔ Ù@ÙEÚÝ[Ý‰ÞWßƒãsêzê—ëXðDñ“ó’ó“õbø"},
    new string[]{"Fa","·¢·¨·£·¥·¦·¤·§·©ÛÒíÀá‚ëŠ‘U‘°Î²¦“Ü–ì˜ìšø›o·ºžž¬m¯V°k°l²X¸ŸÁPÁUÆžÊ†ËtÙH±ááeáwåzéyóŠóŒ"},
    new string[]{"Fan","·´·¹·­·¬·¸·²·«·µ·º·±·³···¶·®·ª·¯·°ìÜÞÀî²Þ¬õìèóá¦¢³„F„G„å…K‡h‰“ŠiŠï‹Ë‹Ì‹Ñé‘Œ’BÞÕ”ó”õ–i–¯—¡—÷˜õšïšøœtJž~ž’Ÿ© í­[®‰±Fµ\¹B¹D¹ »O»o¾u¿œÁ€Ä‡ÅtÅwÅxËXó´Ï›ñÈÒTÓŒØœÜÝGÞNÞxâCçxïcïxïˆï‰÷Yú‹ë¶áë"},
    new string[]{"Fang","·Å·¿·À·Ä·¼·½·Ã·Â·»·Á·¾îÕáÝÚúèÊô³öÐ‚ØÎˆªˆÚ”ë•P•X•\›PœE °­œ±f±}µp¼ÍKÔLÚ“â[åpë„ó„ô™ö„÷›øhúJ"},
    new string[]{"Fei","·Ç·É·Ê·Ñ·Î·Ï·Ë·Í·Ð·Æ·Ì·Èóõòãëèìéåúì³áôÜÀã­ïÐö­ôäé¼äÇöîç³ðò‚n„|…Š‰ŠOŠóŠôŒÐŽüUâö·÷•h•›•Õ–F–{–É—’˜ì™J™¶œdžO éªU¬i¯X°CíÉìð¹A½E¾pç¨Ã^ÃcÃdÆ…ÜØÈQÊ„Ê†ÊˆÎNÏnÅáÑpÑqÒUÕuÙMçšêŠì]ìqïwïyð[ñIòWòaó‘öEü”ü–"},
    new string[]{"Fen","·Ö·Ý·Ò·Û·Ø·Ü·ß·×·Þ·à·Ó·Ù·Ô·Õ·Úèûå¯ö÷çãÙÇ÷÷ƒf·ËÅç‡Šˆbˆe‰ž±¼Š^Š}ŒðŽŒŽËkíª‘°çÞÕ”••S–B–D–Œ—r—±™Jš\åžÇŸøŸþìÜªŠÅÎ²b³W¶l¸j¼S¼ŠÁiÁ‚Á‰ÃRÄÈ†ÉkÊˆÍ_Í`ÐvÓŸØkØrÙSêÚÜmÞMâpä—èMëVëƒîC°äðiðñBñOôš÷aøXüRüvü‹"},
    new string[]{"Feng","·ç·â·ê·ì·ä·á·ã·è·ë·î·í·ï·å·æ·éí¿ÙºÛºÝ×ããßô§‚ªƒt„K„N„Oˆ©ˆù‰âŠ~Œ›o¥’¸Åõ“ž—Q—÷™l›h·º›Íœtœ˜œ½mž–žÐŸuŸ‘ŸÔ Èªh¬S®g¯‚±`´^ºA½ ¿pÃTÅ}Å‚ÇlÌXÌt°öÒƒÖSØNØSÙˆÚRåÌà•ähæ‘çQìbïLïpñTøLøPøiùiÅôüKÒ…"},
    new string[]{"Fo","·ð–¦ˆu—‚"},
    new string[]{"Fou","·ñó¾²»ˆ¡Švžä¼€ÀŒÀÆ]Ð[ë€ø]"},
    new string[]{"Fu","¸±·ù·ö¸¡¸»¸£¸º·ü¸¶¸´·þ¸½¸©¸«¸°¸¿·÷·ò¸¸·û·õ·ó¸³¸¨¸®¸¯¸¹¸¾¸§¸²·ø·ô·ú·ð·ý¸µ¸¼¸¥¸¢¸¤ÊÐ¸¦¸ª¸¬¸­¸·¸ÀíëíÉÜÞõÃõÆò¶ÜÀöÖá¥ÜòäæòÝÞÔÝÊòðöûòóç¦ç¨êçî·ïûÙëôïÙìèõÝ³æÚð¥æââöìðß»Û®Ü½åõíê²»T½ö¸‚Y‚a‚¾‚¿ƒåƒì„_°üß¼…ò†b²¸‡`ˆ}ˆŽˆ¡ˆóŠmŠ•ŠÂŠï‹D‹c‹Ë‹ÑåµŒ @TŽˆŽ“}áÜ·Í»³N‘Ê’h’½’ÑÞå“á”ê–Ž–¢–´–Á–Â–ó—­—Ó—Ú˜_›L›^·Ð›Š›šäßºžÞŸJŸr«c«s¬M­o®i®t®w®}¯ž±G³Qµy¶O¶·J¸c¹A¹[¹r¹…º…»™¼J¼”¼›½E½n½•½š¾”¿`ÀbÁJÁÃiÄwÅ€Æ]Æ…ÇCÇXÆÎÈQÈiÈƒÉ’ÊÌ’ÍbÍkÍ|Í—ÎlÐuÐ“Ð•Ñ}Ñ‡ÒLÒiÒ„Ó‡ÔcÖDØfØ“ÙMÙxÙŽ·ÑÛ~ÝPÝoÝ•Ý—»¹ßß‘àGàMà~áKáUáœâaãRãVäžå‡å˜ïÂÚâê‚÷¹íhíví‚î\ïOïTñ€ó‘ôfõHõVõvövøDøIøWøqùfù›ûŸüAüF"},
    new string[]{"Ga","¸Á¸ì¼Ð¸Â¿§ÔþîÅÙ¤ê¸ÞÎæØæÙßÈ‡Q«VÜˆáåmôp"},
    new string[]{"Gad","®h"},
    new string[]{"Gai","¸Ã¸Ä¸Ç¸Å¸Æ½æ¸Èê®ÛòØ¤Úëêà_ì„÷„ø¿ÈŠ¡Yã“©•|–qºË˜¢˜£Æû[­y®„´oµ‹½i½wëÜÇDÈ‘ÉwÔ“ØdÙWÙ^à@â}æYéuºÒêdëBº¡"},
    new string[]{"Gan","¸Ï¸É¸Ð¸Ò¸Í¸Ê¸Î¸Ì¸Ë¸Ó¸Ñêºôûí·ðáãïÜÕß¦ç¤éÏä÷äÆÞÏÛá¸öqÇ¬xœÎ‚‰ƒ÷„Q…î¼éŒ¼Œ¿ŒÀŽÖå’Iº´”—U˜o™gº¹›N›¿lž¸«\«q°‘±Y¶’¹C¹mºTº•»ˆ½CÆQÍHÐrÔlÖPØJÚCÚMÚsÞ|âFåDïó_ôvöx÷h÷ øN"},
    new string[]{"Gang","¸Õ¸Ö¸Ù¸Û¸×¸Ú¸Ü¸Ô¸Ø¿¸óàî¸í°¿ºØøƒé„‚ˆÕˆþŒù‘Þ‘ß¿¹’â—ž˜œÏŸ€ ± Â è¯I³M´L¾VÀ“À °¹âGä“æsêlî@ñþ"},
    new string[]{"Gao","¸ß¸ã¸æ¸å¸à¸Ý¸á¸â¸ä¸ÞÛ¬Ú¾ê½çÉØºéÀï¯éÂÞ»„Æ…Ì¾Ì‰ùz•±˜‚˜°™R™™²ºÆœõ»ª‚ªˆ°w²Gµ†µ‡¶J¶Ž·X¹l¿cÁoÅVÇÝïË›Õaä†æ€ízðpó{úkúüŽ"},
    new string[]{"Ge","¸ö¸÷¸è¸î¸ç¸é¸ñ¸ó¸ô¸ï¿©¸ì¸ð¸ò¸ê¸ë¸í¸ÇÒÙ¸õíÑ÷Àò¢ñËÜªò´ÛÙïÓØîô´ØªàÃëõë¡æüÛÁ½éÞà‚€„ý¿É…Ã…Ï†þ‡S¸ÁÍ‘á‘ë’M’š”R”š–q˜†™ ºÆœèœð» ³ · çªnª˜íÀ¶…¹w¼vÃIÄ—ÅZÆŒºÊÉwÍxÑ\ÓkÔ†ÖYÖgÝ‘ÞPâ›ãtãxædækæŠ¼ØîþéléwéxÕ¢ì‘íRíkíuîMòZ÷ÄôŸõiõsö÷…ømøwøùB"},
    new string[]{"Gei","¸ø"},
    new string[]{"Gen","¸ú¸ùßçÝ¢Ø¨ôÞ“^“j"},
    new string[]{"Geng","¸ü¸û¾±¹£¹¢¸ý¸þ¹¡âÙöáßìç®ƒ¿º„jˆíya’ª’ù•œ—Ô›ÊŸ‰®uÓ²½b½c½Ž¾¿KÁ}ÇcÈ@ÙsÐÏàDàQîióiõ†ùˆûf"},
    new string[]{"Gib","†Ö"},
    new string[]{"Go","†ñ"},
    new string[]{"Gong","¹¤¹«¹¦¹²¹­¹¥¹¬¹©¹§¹°¹±¹ª¹®¹¯¹¨ºìëÅö¡çîò¼…@…C…šßÛ†y†ß‰bŒmŽ³ÞÃã‘E’–r¸Ü–íœ|ŸË´bºT¼k¼tÁ‡ºçòËÓyØ•ÚCÚM¸ÓÜpÝ\äUì–ó•ô„ýŠý"},
    new string[]{"Gou","¹»¹µ¹·¹³¹´¹º¹¹¹¶¹¸¾äá¸ì°èÛ÷¸êíçÃóÑÚ¸åÜæÅóôØþ‚×ƒÚÇø…^…éˆx‰òŠ¥’]¾Ð“k“Â˜‹›tœÏŸµ«vº¾—ÂTÂUÂVÆ™ÍmÐÑÓMÔ_ÔØmØxÙÝ@âhã^ëgíxõLøzûYð¶"},
    new string[]{"Gu","¹Å¹É¹Ä¹È¹Ê¹Â¹¿¹Ã¹Ë¹Ì¹Í¹À¹¾¹Ç¹¼¹Á¹Æ¼Ö¹½èôð³ãééïáÄÝÔðÀ÷½îÜëûôþßÉöñÚ¬êôî­ì±ïÀêöðóõýòÁî¹ØÅ‚ïƒlƒó„½¸æßß†f†g†˜†Ø†åˆØ‰à‹²Œ½gHë’M’_¿Ý–¾—›˜b˜€™O™¤›}›ü»¬žJžkŸ‚ð­¸Þ°–±W³‘´hµ¶™·Y¸š¹‡ºH¼M¿SÁBÁlëÒÃ™ÅV¿àÆ‚ÉuË[ÍvÐM½ÇÔbÙZÝLÝMÝžßEââ’ådíî™ðkð ÷»õYöAøù]úXü‰"},
    new string[]{"Gua","¹Ò¹Î¹Ï¹Ñ¹Ð¹ÓØÔßÉëÒð»èéÚ´ƒÖ„Ž„œ…³†F†J†§ˆqã·’ìšOŸ…Ÿ°½\¾ ÁGÁLÉàÆ‚ÔŸÕ ÚoÛ|ã”äTèïNïWòmøŽÀ¨"},
    new string[]{"Guai","¹Ö¹Õ¹ÔÞâ…¨ßà‡ˆ‰øs–¡–Ê¹yÁL"},
    new string[]{"Guan","¹Ø¹Ü¹Ù¹Û¹Ý¹ß¹Þ¹à¹Ú¹á¹×ÂÚîÂñæÝ¸ÞèäÊ÷¤ðÙÙÄO´®…jŠþ ¡‘T‘×“¥ÎÓ¹ûèæ˜À™Â™àš¯ÂÙ›Œ›ýœS‚ ƒ¬g­¯p¯°H²•µeµ¸A¹`¾]À•ÅoÝÑÈXÉFÒ‹ÓQÓ^ØžÜIÝ„ßkå]æšè…érévêKêPëqð^öŠ÷b÷}øAùJûX"},
    new string[]{"Guang","¹â¹ã¹äèæáîßÛë×ï‚UƒZˆŠ­ŽÚV»ÐÀ©’•“Ñ”Uºá™¤™õ›²äêž»žÓžÕžÖŸD«E«‡³qÅQÅSÆšÚ‡Ý_Þ‚ã üU"},
    new string[]{"Gui","¹é¹ó¹í¹ò¹ì¹æ¹è¹ð¹ñ¹ê¹î¹ë¹å¹ç¹ô¿þ¹ïÈ²âÑå³èíØÛ÷¬öÙð§ØÐæ£êÐóþêÁwÎ±æ‚Îƒ^„£„¥…QØÑ…T…‘ÍÛˆ’Š¹ÍÞ‹‚‹¥‹¾Ž@ŽQŽ`Ž¢Žë@i“±“Ê”Š”‹•Q–_Î¦—Ë—Î¸Å˜œ˜­˜²˜³™u™™™Æ™ÍšwšðãíÍÝœˆœÄ‘«•­Y­„°I²Z²n²z³uÆíµƒ¶W·˜¹Kºl½}ÀL»æÃvÄ„Æ—É}ÌlÍŠÎšÏjÑOÒ^ÒŽõûÓmÔŽÖdÙFÚbÚ‘õêÜ‰ßžàFé|ê{ÚóëvíWòoôhôkõq÷Z÷iøWø_ø`ý”"},
    new string[]{"Gun","¹ö¹÷¹õöçÙòíÞçµØ­¨—œ»ë»ìœ†L¬g­e±š²O¾i¾É€ÊFÐ–ÑrÖÝåKï¿õPõ…öŠ÷¤"},
    new string[]{"Guo","¹ý¹ú¹û¹ü¹ø¹ùÎÐÛöé¤ñøÙåâ£áÆÞâàþßÃë½òäòå»®†F†J»£†©‡Hàí‡ë‡î‡ñ‡ó‡øˆÍˆå‰Ž½˜œ«‘I´ê“”šèÛ—ë˜¡™¤»î›ýœuXã¯†²Žºl¼@¾[ÂƒÄBÄNÄsÇ‘ÊbòâÎÏXÙùÑxÝ{ß^âuä˜åèJï¾ðRðŸ"},
    new string[]{"Ha","¹þ¸òÏºîþRÏÅºÇŠUŠožéâ³Îrãx"},
    new string[]{"Hai","»¹º£º¦¿Èº¤º¢º§º¡º¥àËõ°ëÜì…õßÔ†ã‡¯‰h’™üŸQªn½wß€à@áVéuºÒîWò¤ï™ðŽñ”ñ›ºÙ"},
    new string[]{"Hal","a"},
    new string[]{"Han","º°º¬º¹º®ºººµº¨º«º¸º­º¯º©º²º±º³º´º¶º·ºªÚõÝÕÞþå«ãÛñüòÀìÊò¥êÏ÷ýœÎ‚þƒË„T³§…{…î†c†i‡•‡öˆ¥ŠÎ‹©Œå—²Ç¶å¸Ð’I”êº•~•ˆ•Â—U—c—ß˜o™÷ša›Nãï›¿›È›Û›þäÆÌ²hä÷¶Èž©Ÿß ’ªR¬H¸Ê®]°y±Ží·¸’¹b¼`ÃQÇtÊGÌkÍHÍ”ÎKÎLÎ‘Ö›ØEØJÜŽÐùâFâjäIädäwîÔé\êRê\ënìyíní™îMîhîuî›ñHñUòAô_õAøNú["},
    new string[]{"Hang","ÐÐÏïº½º»º¼¿Ôñþãìç¬çñˆœŠsý”ãèì¿»ÀÇ¸‘¹V½W°¹ÆfÍaØ˜Þ†ß’ôûî@ôŒ"},
    new string[]{"Hao","ºÃºÅºÆº¿º¾ºÂºÁºÀºÄºÑ¸äê»ò«å°àãòºàÆð©Ýïå©Þ¶‚ÛƒŸ…ë†S»£‡_‡sæ¤hˆ•a•‰•±•µ•¼•Ø—·œBœéœõ»ž®ª|ª‚¸Þ°€°‚°…°ˆØº¶m¸h»DÂGÂ|ÄzÅVÆ’ËAË^ËrÌ–Ì—Ï–Õ’×qàzæeæ€çî—ö‚"},
    new string[]{"He","ºÍºÈºÏºÓºÌºËºÎºÇºÉºØºÕºÖºÐº×ºÊºÑºÒºÔÏÅàÀÛÀîÁôçãØò¢ÛÖÚ­æüêÂÞßÃºô…ô†J¹þ†Y† †¿†Ûà¾‡m‡˜ˆ†Šº¦ŒyP²Ô’u’š½Ò”—–­¸ñ—æšBšÎÇ¢œfœz¿Êœ¸¼ŸZŸŒŸ¿ŸÀ _ e çªC°F°±A±B¹è´E´¶…ºK»t»—¼vÀU½ÉÂG¿ÁÈMËrÞ½òÂÐ«Î˜ÏšÐŽÒ‡ÔXÔZÔ†ÖyØ€ÙRÝ`Ý éûÏ½àAãFèYéuêHãÒëa»ôìeìfìgíHîMïðgðšô]ôŸ÷…ù]ùŸúKúQûSðÀûiûýLý[ý†ý˜"},
    new string[]{"Hei","ºÚàË‹Ï¦ü\ºÙ"},
    new string[]{"Hen","ºÜºÝºÞºÛäßç‡Œ’‹ÏÆôÞÔ‹ì•"},
    new string[]{"Heng","ºáºãºßºâºàÐÐèìçñÞ¿ä†‘ˆýŠ¬a™M›êžîªBÃtÃ†ÙêèUø’ùCûa"},
    new string[]{"Ho","Y"},
    new string[]{"Hol","b"},
    new string[]{"Hong","ºìºäºåºçºéºêºæºèºëÚ§ÙêÞ®ãÈÞ°ÙäÝ¦ãü›…š…·…Æ…Ë†M†y†ß‡«ˆ˜ŠkŠ¼ŒfŒâŽcšã“E“Ð•{Íô›K›Ä›Í¸Ûœ|œ‚~µ¹ž¿Ÿp«Y«a­˜³{³…¸f¸sºC»Ž¼t¼‡¼˜½“À€Á‡ÁŠÁÂoÅ|ÆyÈ‡ÈˆÓÖhØAØDØFÜŸÝ“ÞZâvãpäUäfåébé{é•é—ë”ëŸìô\ô„ø™üZ"},
    new string[]{"Hou","ºóºñºðºíºîºòºïö×óóÜ©ááåËô×÷¿ðú…Ë…éˆ‹Ž«›• ê²T³@ÂFÂJÄDÈ‰ÔÚ¸Ø_àCàjãæAðfõ`ö\÷c÷ýJ"},
    new string[]{"Hu","ºþ»§ºô»¢ºø»¥ºú»¤ºý»¡ºöºüºûºù»¦ºõÏ·ºËºÍº÷»£ðÉÙüâïð×óËìæìè÷½ä°ìïõ­çúàñìÃéõð­ìÎõúðÀâ©ã±á²äïì²ßüéÎ[‚sƒê…I…O†¼†Ø†Û‡F‡P‡©ˆ~‰Ö‰ØŠ¯Šý‹|‹¬‹­ŒŒŽŽÄuHm‘ï‘ñ‘ò‘ô‘õ’_“‡“ª”N•O•U•÷–—ü—ý˜«šXš£ãé›R›Z›~›´›üœWœXœûGžCž€ŸWŸÚ­•®@µC·‚¹}¹”ºn»‡½`½œ¿S¿T¿eÓðëÒÄŠÅnÆSÆUÂ«ÜÌ¿àÆ~ÈLÊSÊdò®ÌÌ•Î™ÐkÓ{ÔSÖ—×oÐíØmÜ à‚â’änåtåæLîÜëa¹ÍëiëŒí_í’îgðbôEô–öUö{÷s÷ŸøUø‡ù]ù–úCúKúXûI"},
    new string[]{"Hua","»°»¨»¯»­»ª»®»¬»©»«»íîüèëæèí¹Ù¨„Øå…ÅÍÛ‡WˆµŠ£‹N‹O‹½‹ÃÑ§ŒW†ÕÒ“Š“®“çµÐ–—É˜¥˜å™Šä«±Òªœ­L®‹®“³“´hïý¼@¼AÀEÄBÅpÆ_ÈAÉJÊyÌfÌsÎ”ÓiÔ’ÕjÕ–Õ Öœ×fÝ{âDâEänåkçfò‘ô‰õqöÙú†üX"},
    new string[]{"Huai","»µ»³»´»±»²»®õ×Ý†Fà°‡]ÛÚÅ÷‰²‰Ä‘¯‘Ñ™ÆžxÂjÌxÌ|Ñ‘Ñœ"},
    new string[]{"Huan","»»»¹»½»·»¼»º»¶»Ã»Â»Á»À»¿»¸»¾äñâµß§åÕöéÛ¨÷ßå¾Û¼ïÌà÷ä¡ÝÈçÙä½†¾†¿‡È‡õÛùˆâŠJŒAŒ~`´ŽwÑ‘¤‘×“QÔ®”k—h˜¬™öšZšgš÷œo‚È×¹àžðŸ¨ íªB¬~è¥­h­’¯ˆÍîÑ£±±š²`²o¼]½b½Œ¾ÀQÁvÃKëäÇBÈPÉVËÎŒÐS×’ØhØoØ}ØŽÝkÞSß€à ãhæDèGéIêXêaëfëqóOõŒöZödøbùJûXûqðÙ"},
    new string[]{"Huang","»Æ»Å»Î»Ä»É»Ë»Ê»Ñ»Ì»È»Ç»Ð»Í»ÏÚòëÁäêóòáåöüåØñ¥äÒó¨è«‚µƒÆ†Åˆð‰E‰ŸŠN‹hŒr¢ŽxUé“N•s•Í–M˜R˜n™¤›R›²œêžêŸºŸì pª¬‰°°Œ·k¿mÅŠÃ¢Ã£ÈÐYÔ…ÖWÖeÚ‡å–æwçuéBí‹ðcòböm÷UúŠüS"},
    new string[]{"Hui","»Ø»á»Ò»æ»Ó»ã»Ô»Ù»Ú»Ý»Þ»Õ»Ö»à»Û»ß»×»ä»²»Ü»â»ååçä«çõÞ¥à¹í£ßÜêÍãÄ÷âÚ¶ó³Üîä§ßÔò³ÜöçÀÝƒaƒª…R…¡‡G‡j‡v‡‚‡¤‡ß‡éˆH¶é‰™‰ÄŠî‹^Œ@Œ“ŒáŽ¹@hj¡¢Úo{‘}‘Î’’“]“Ö•Ÿ•Á•þèí—Û—ò˜ž™B™b™m™u™®š§š«›i›x›‘»Áœ“œóŒÒèž`ž¾Ÿ@ŸCŸFŸŸ˜ S Zª›¬q­_­g¯`¯ð©íõî¡²N²~µ˜·xº_½}ÀDÀLÁ™ÁšÂEÂPÆUÉLÊ]ËCËDË™ÌlÌs³æÍYÍzÍ ÎšÐ„Ñ‹Ò^ÔÔœÕdÖM×M×e×f×wØYÙVÝxÝ{Î¥Þ’ßDß`ã„çiçžêTê_ëDìuíWífí}Î¤î_îœðdõtö™ýHýIÀ£"},
    new string[]{"Hun","»ì»è»ç»ë»é»êãÔçõâÆäãÚ»‚[‚“‡õ‹Gù¸Çù»ÓÀ¦’ä“]À¥•e—p—y¹÷—•š‰›÷œ†œ¡œ³Ÿ[Ÿk¬q±d²E²J¾i¾r¾‡¿ŒçµçÅÈÊMÓoÕŸÞFé’î‚ðQðaý@"},
    new string[]{"Huo","»ò»î»ð»ï»õºÍ»ñ»ö»í»ô»óàëïìñëØåÞ½ß«ïÁó¶îØâ·å‚i„Š»¯…¿…ô…ü†Ø‡—‡ÉŠ_Š£°ç’»’î“n”N”ü•ë—ë™Š›[œ­tèžCžmìáŸZ«@ð­°\±n²ˆ²‘´žµœ¶¶„·‚ºWÂhÄNÄsÅGÅŸÈuÉ^Õ’ÖfØmØ›Ô½Úoß^ß˜â€åxèZéXëbëoì[òdôr"},
    new string[]{"Hwa","‰þ"},
    new string[]{"I","U"},
    new string[]{"Ji","¼¸¼°¼±¼È¼´»ú¼¦»ý¼Ç¼¶¼«¼Æ¼·¼º¼¾¼Ä¼ÍÏµ»ù¼¤¼ª¼¹¼Ê¼³¼¡¼µ¼§¼¨¼©¼¢¼£¼¬¼»¼¼¼½¼­¼¿¼À¼Á¼Â¼Ã¼®¼ÅÆÚÆäÆæ¼ÉÆë¼Ë¼Ì¼¯¸ø¸ï»÷»ø»þ¼¥»û»ü¼²ÛÔä©öÝåìì´êªöêïúí¶ð¢ê«ò±ÚµóÅôßóÇØÞß´ÝðõÒáÕÞáõÕö«ßóçÜÜùñ¤çáî¿Ø¢ÙÊÜ¸Ù¥êåé®÷ÙßÒÞªêé÷äæ÷éêá§Ø½ê÷ØÀÜÁßâMU‘³ÒÐ‚Âƒ_ƒÎ„W„Z„ˆ„©„Þ…hß²…uØÈ…¯…»…è†À†æ‡\‡ˆjˆ…ˆô‰J‰€ŠjŠ Š¸Œ¨¾ÓŒÛŒïc³ŽNŽ©Ž×Žó^åæŸ²Âî¯ê‘¢‘¼‘ÕÒ¾“V“Ä“Ø“ê“ô”D”Œ”ª”ú”û•¸•Ì–ˆ—mÆå—ù˜O˜Š˜œ˜Û™C™W™o™v™‹™›™Ãš©›D›‹½à›ùœgœ–PTçúž†Ÿd äªEâ¢­D­^­u®‚´Ã¯sñ©°U°^°n°uî¥²]öÄ´‰¶I¶S½Õ¶·I·]·b·e·m·}¹U¹œºsºuôÒ¼_¼o¼¼‰½Y½o¾@¾ƒ¿]¿ƒ¿ŽÀMÀ^½áÁYÁaÁbÂcÂfÃhÙõÃÄlÅUÅÆIÆaÆnÆˆÆ–ÇgÝ½ÈWÈ—ÉaÊDÊmÞ­ËEËj½åÌIÌRÌnÌzÌ~À¯ÎaÎŽÏlÏÏ„Ï…Ñ_ÑwÒHÒQÒˆÒ‰Ò—ÓJÓ]êèêëÓfÓsÓ‹Ó“Ó›ÕHÕ‚Õ‘×I×^Ú¦ØCØGÙ}ÙŠÚlÚ|Ú–ÛEÛaÛeÛpÛˆÛ”ÜQÜeÜuÝ‹ÞUÛ¤àBàœã‚ãšåZå‰çgçˆèWèi¸ôëHëYëuë|ë}ìPìVì“íZí‡ïWï|ð‡òTóKÆïônô‚ô‡ôŠõJõŸöSöaö›÷C÷D÷q÷‚øKùHùúWúaúnûAûnýRýTýUýVýW"},
    new string[]{"Jia","¼Ò¼Ó¼Ù¼Û¼Ü¼×¼Ñ¼Ð¼Î¼Ý¼Þ¼Ï¼Ô¼Õ¼Ø¼ÚÇÑ¼ÖîòÝçåÈê©ä¤ïØðèí¢áµõÊØÅÙ¤ëÎóÕçìðýÛ£ôÂòÌ‚íƒr…­ßÈ¿§†kˆ]ˆ®ÏÄ‰ìŠA‹TŒ_Ž·Ëð‘æ’S’zÑº’~Þ×êüÐ®’¶¿«“a“ü”Ï”ÐÏ¾—k—Ý˜\˜k˜–™xš¹›v›Ñ Çªmªo«w¹k¼OÂ_ÃÄ`ÇvÍÎrñÊÑWØjØ†ÙZÛOàPâ›ãeãxäeæ‰îþîRî]îaò¡ïðšñ{ñ˜Âæ÷ºø”ùGû“"},
    new string[]{"Jian","¼û¼þ¼õ¼â¼ä¼ü¼ú¼ç¼æ½¨¼ì¼ý¼å¼ò¼ô¼ß¼à¼á¼é½¡¼è¼ö½£½¥½¦½§¼øÇ³¼ù¼ñ¼í¼ã¼ó¼î¼ï¼ð½¢¼÷¼ê¼ë½¤ôå÷µê§ÚÉêðèÅëìõÂçÌÞöê¯ë¦ÝÑöäóÈÚÙé¥àîå¿ñÐõÝíúåÀðÏÝóÙÔïµäÕü‚k‚›‚¡‚ßƒcƒ€ƒïÇ°„‡„„¦„§„ª„«º°‡ØˆÔÇµ‰A‰q‰¤Š¦Š§åîŒ{Ž¥ŽÔ”É½‘â‘ì’³’þ“B“b“ì”W”s”ð•©•üèÔ–ç—g—Ê—ß—ä˜c˜Ù˜ó˜ö™Z™z™‘™Òššž›–›×œ\œpœ—ÀÄu¾žEžRžhžˆžŒžŸÒŸæ  êùª\«l«…¬{¬‚±O²R²v²{²€´D´´–´šµM·S¹a¹{óðº]º†»E»W¼G½€¾}¿V¿ ÀOÀoÀwÏËÂžÅ[ÅžÆDÝ¢ÈGÈ‚È…È“Ê`ÊzÊ—Ë]ËuÌ‚ÏMÏ•ÑIÒMÒOÒ}ÒŠÓSÓVÔdÕÖGÖˆ×P×t×vÚÚØ]ØbÙ`ÙvÚ{Ú™Û`ÞYá_ázâJâVâ]ã‹äEä[ägä’åXåbåså€æGæIæ~çZç‰ç™èBèaèbè{è~èƒè—Ç®éfégÏÕëUìyíKí[ídðTðeñJòqå¹ôCöröxööž÷œøZùpúYûxûyû{û|û…übüjÛÈ"},
    new string[]{"Jiang","½«½²½­½±½µ½¬½©½ª½´½¯½®½³Ç¿½°ºçôøíäçÖêññðç­ÜüôÝä®‚×„ß…G‰D‰¬‰áŠXŠ\Œ¢Úx‰Š™“°“À–t˜ª™^™ºš™@{ª„®{®–®Ÿ¼T¼t½{ÀPºìÁžÄvÈwÊ@ÊYËKÎ…ÏQÑHÖvÖ˜ánáuí\îŽ÷F÷š"},
    new string[]{"Jiao","½Ð½Å½»½Ç½Ì½Ï½É¾õ½¹½º½¿½ÊÐ£½Á½¾½Æ½½½Ã½¼½À½¶½Î½Ñ½·½¸½È½Â½Í½Ä½ËáèÜ´ÙÕòÔë¸á½õÓæ¯ð¨ÜúðÔàÝõ´Ù®äÐöÞÞØÇÇÇÈƒSƒeƒ‚„¤„à„äÈ´…s…ÓÒ§†Ì†û†ý‡E‡U‡„Ñý‹´‹É‹Ð‹ùÑ§ŒWjÛõþŽBkˆ‘x‘‹‘¢’›’¹“¼“×“è”‡”œ”©”º”¼”Ò•w•¯•Ý˜È˜ò™Ëœ©œò]‰²¼¤ž•ž«žìŸ}Ÿ”Ÿ÷ª—«„­d°‰°³C·X·p·•¸‹¹RºŠ¼m½gÀUÀq¾ÀÄ_ÄzÄ‰ÅTÆLÆ›ÝÄÈcÊwËŠÌ—ÏfÏtÒÒ™ÓXÓŠ×K×_Ù]ÚˆÚŠÛ]ÜFÝ^ÞBÞIàzá†á ãqç€ïœòœófõo÷RøŸùaú„úŒæù"},
    new string[]{"Jie","½Ó½Ú½Ö½è½Ô½Ø½â½ç½á½ì½ã½Ò½ä½é½×½Ù½æ½ß½à½ê½å¼Û¿¬½Õ½Û½Ü½Ý½ë½Þ¼ÒÙÊèîà®Þ×÷ºôÉò»àµò¡öÚæ¼íÙÚ¦æÝðÜÚµNº¥‚Œ¼Ù‚Í‚Üƒr„f„g„o„Â…m…Ãßó†‡†—‡»øˆêˆûÆõËýŠo‹d‹m‹}‹‘Œ¨ŒÃŒîŒô›ºËŽOŽYŽ^ŽÑŽàÈð…’Mµ£Ê°’÷“ƒ“ø”O”T”â•Môß•Ì—A—¸—ô˜H˜P¸Å˜m˜‹™w™Ãš²›­œf¿ÊœœœïŸ® ÏªEâ³«d¬p®v¯C¯^°X³VíÀµ@×æ·M¹¼®¼v¼½Y½eÀTæüÂcÃÅ‹Ç}ÇÉ•Í„ÍŽÍÎaÎfÏ˜ÏÐVÐwÐ|ÐñÊÑKÑ\Ñ›ÓnÓ“Ô‘Õ]ÕmÖŠÚlÛOÛdÞ—ã]å|æOïÇëAëeìŒì“îRï÷Øô‚ôõ^ù™Úà"},
    new string[]{"Jin","½ø½ü½ñ½ö½ô½ð½ï¾¡¾¢½û½þ½õ½ú½î½ò½÷½í½ó½ý½ùâÛèªâËéÈñÆÝÀÝ£ñæàäçÆÚáæ¡êáêîµ‚BƒHƒqƒƒ»„B„³„Å…Ò÷†‚‡žˆ²ˆü‰ƒ‰½Šú‹¦‹Í‹âŒƒûŽ„®‘[“|”Ü•x–‡˜cšVš›»œÃWäøŸ¥ a«ƒ¬Q¬n¬’­\­n±M³\µ‰¸…¸’¼Ž½G¾o¿NÀßÅ]ÇMÇžÈBÉ“Ë|ÓPÓbÔCÖ”ÙÚBßMáŽâYäuå\îÄï·ð~ñ^ûvüTý„"},
    new string[]{"Jing","¾¹¾²¾®¾ª¾­¾µ¾©¾»¾´¾«¾°¾¯¾º¾³¾¶¾£¾§¾¨¾¬¾±¾¤¾¥¾¦¾¢¾·¾¸ëÂâ°ÚåëæåòØÙã½æºëÖÝ¼ÙÓìºåÉö¦ãþSŠ¤‚\‚Š‚ýƒ ƒô„q„³„ÅˆgˆiˆlŠnŠøŠùŒcŽyŽÁ†½‘ “÷”ìêÉ•Ç•ß—J—}™YéÑš„ÊÏ›G›H›·›ÜœQžDžsÌþŸN GªS«E­E­Z­`¯d¶p¶“·¸t¸x¸‚¸„óä»~½U½›Â€Ã„ÇGÇoÈòßÏ‚ÕeÛVÞŸÐÑätçRÚêê€ìiÇàìmìnìoîKîeîiïIó@öLùXù~ù‚ûû—ü "},
    new string[]{"Jiong","¾½¾¼ìçåÄ‚CƒTØçƒÕƒ×‡åˆsÛðˆ·Œl‘û•Q›s›Ó°ž]êÁŸKŸ Ÿ¡ŸÉŸâŸü E½N½ŸÅQÅSÌSÌWÑ•Þ›ã}ævîyïGñoñ’"},
    new string[]{"Jiu","¾Í¾Å¾Æ¾É¾Ã¾¾¾È¾À¾Ë¾¿¾Â¾Ç¾Ê¾Á¾Ä¾Ì¾ÎôñðÕõíÙÖèÑèê÷Ýð¯ãÎà±LX`‚w„—„ó…B…E…YàÝŠeŽýGH³î‘W’º“A“[“š–`–w–Í˜Í˜þš”šð›CäÐœ© ¬®o·T·c·•¼j¼m¼‘¿ŠçÑÅfÅiÈ\òøéNíƒôböJøFúûýn"},
    new string[]{"Jou","™ã"},
    new string[]{"Ju","¾ä¾Ù¾Þ¾Ö¾ß¾à¾â¾ç¾Ó¾Û¾Ð¾Õ¾Ø¾Ú¾Ü¾å¾Ï¾Ñ¾ÔÇÒ¾Ý¹ñ½Û¾ã³µ¾×¾Ò¾á¾æÙÆõ¶ñÕåðêøÜÚñÀì«ï¸é§ÜÄè¢Þäé°ö´ôòÜìöÂåáéÙõáé·÷¶îÒÚªèÛ³ð¼Øþ‚I‚e‚˜ƒhƒâ„H„¡„è„û¸æˆRˆoˆ¿ˆÏ‰±½ãŠŠÛÈ¢Šè‹JŒŠŒÕŒøŒþ‡ŽelßþDIì‘§‘Ö’]’‡¹°’¤’±’º“E“T“þ”H•Z—x—º—»™h™Î™ÛšjšÁšÆ›t›†›®›ôœHÇþœ¦ÞŸhŸq „ Ê ó«~¯Y±röÄ³^¶€¸M¹_Â¨ºtº–»c»‰»ÁDÂ`Â‹ÄKÄ”ÅeÅ‰ÝÏÈgÈ{ÝäÉXÉaÉ›ÊVÌ^Þ¾ÌŽÌ˜ÍiÇùÎAÏJÐÒzÔnÕ‡ØeØ‹ÚkÚzÚ~×ãÚ ÛBÛRÛgÛžÜFÜMÜvÝ@Ý]ßš×ÞàTàYà`àuÛ¸â ãIäzä|äèL³úé…ê³ûërïZñuñxñòœóM½¾å÷õLõQõX÷‰ø~ùVùqù‰úGüŸýAýe"},
    new string[]{"Juan","¾íÈ¦¾ë¾é¾è¾ê¾ì¾îÛ²ïÃîÃïÔáúèðä¸öÁ„»„Ì„æ…Û‡üˆ±ˆ»ŠFŠ¤ŽQŽ`Ž™ƒ€³‘g’Ôß§–K—]—¨ãù›ûŸ]ª™®CÕçÑ£íü±’²C½v½¿xÁIÁ\ÃÄCÄ–ÇšÈTÊ^ÈïÊtòéÑZÒN×zÛmÉíägämäŸæŒçé—ëhëvìœíjï…ðCùJùN"},
    new string[]{"Jue","¾ö¾ø¾õ½Ç¾ô¾ò¾÷¾ï¾ó¾ñ¾ð½À½ÅèöàåéÓàµõûØãìßÛÇïãâ±çåáÈÞ§àÙÚÜõêæÞØÊÒÒ|‚à„]„ä…Z…¨…É†­ˆ«‰®‰øŠxŒHÇüŒÖŒØŽ@ŽD{ž‘‰‘•‘Ý’¢’Á“Þ”Çèß™@™ê™þš€šÜ›Q›‰ˆžŸŸ]Ÿ}ŸØŸ÷ u “¿ñ«P«i«k¬œ­W¯N¯‹²œ²Ÿ³O·‡Ñ¨½^½~¿”¿›Ä_Ä”Æ`Ê…Þ©ÍDÍXÍÜÏpÏqó½ÐœÒÒ™ÓXÓÔEÕo×HØÚbÚkÚ‘ÜBÜFÜjÝ^½ÏßIâfç~çè‘éQé êIã×ãÚ÷³íXñiòjóYóp÷Z÷¬ø_ø`ùŠú€ý™"},
    new string[]{"Jun","¾ü¾ý¾ù¾ú¿¡¾þ¹ê¿¢¿¥¾û¿£¿¤óÞ÷åñäÞÜƒy„òÔÈ…Íˆ­Š®Œ”‘®”hÑ®•€—T›JžFŸaŸlŸóâ¡¬B®°—°˜´A¹„¹‰ÇqÈšÊ^ÍSÎDÐ‚ÒŸÙêÜŠâxãzã—ä]å‹ê}öÁëhðKðžòEõz÷ùQùRùUûŠûŽý”"},
    new string[]{"Ka","¿¨¿¦¿©¿§ëÌßÇØû…íˆšçìÐ_ÑQãl"},
    new string[]{"Kai","¿ª¿«¿­¿®¿¬ÛîØÜï´îøïÇâéâýÝÜ„P„’ÛÀ„ÑºÈ†Ë†þ‡i‰NŠKÌŽ¯Ôð÷•°ºË™üš@œf¿ÊäÛžGžÍžÏžýÐ_ØMÝaå|æbæzç˜é_êGê]ïôïa"},
    new string[]{"Kal","f"},
    new string[]{"Kan","¿´¿³¿°¿¯Ç¶¿²¼÷¿±íèê¬Ù©î«Ý¨ãÛ‚°ƒÝÛÉº°ˆÉ‰A‰d‰{€®§’X–Ý™‘šKšM±O²™´U´|¸ƒÝ²ËWÐb×tÝ|ÝÝÞRêRìyîƒðWÏÚý"},
    new string[]{"Kang","¿¹¿»¿¸¿·¿µ¿¶¿ºîÖãÊØø…H‡ã¿Ó·Ü‹¢Üý“•º¼˜±ãìo è³T·^»~»ÄÜ{ß’â‚ç_é`êlóa÷K"},
    new string[]{"Kao","¿¿¿¼¿¾¿½èàêûåêîí@Ïì°ÞØ¸ã“×”Ž˜‚éÂ›ŸŸ\ŸÀ _·XË^Ë›ÓˆäDó}õwõ‘÷Š"},
    new string[]{"Ke","¿È¿É¿Ë¿Ã¿Æ¿Å¿Ì¿Î¿Í¿Ç¿Ê¿Á¿Â¿Ä¿ÀºÇã¡á³òòç¼òÂéðñ½îÝë´ò¤î§ï¾ïýçæ÷Áðâà¾äÛæì„w„Ä„Ë…\¿¦ˆÑŠÄŒ¡QºÁŽP…ÙÚ“U“t”¨˜}˜Ê™üšMš£šÎœfžGžÜ ˜ É¯zîÁ³`³‚íÙ´R´h´žµL¸Sºœ¾~Á˜ÃmÅ‹ÈdËPÐ_ÐŽÕnÚÝVáfâŽãxä˜åHîþï¹îWîwòSý"},
    new string[]{"Kei","¿Ì„w„Ä„ËŒ¡"},
    new string[]{"Ken","¿Ï¿Ð¿Ò¿ÑñÌÛó‰¨‘©’õºÝ«³wÃGÃ\ÑyØcØ~åoí ñýýlö¸"},
    new string[]{"Keng","¿Ó¿Ôï¬„´ˆcŠRŠsìþ’®“@“¾š  ¾³n³wíÊ³³™ëÖÃ„ÕUÛVãsäLå”çHîïêl"},
    new string[]{"Ki","]"},
    new string[]{"Kong","¿Õ¿×¿Ø¿ÖÙÅáÇóíˆÂŒ^£—¾›ï³M³œñ·ÁzÇ»ÜwåIìùy"},
    new string[]{"Kos","W"},
    new string[]{"Kou","¿Ú¿Û¿Ù¿ÜÞ¢ÜÒíîóØßµØþƒã„›„¼åý‹žŒt“D¿æ“¸””šªœÏA±r²]²g·¸lºpÆ’Êfâ@æ–úd"},
    new string[]{"Ku","¿Þ¿â¿à¿Ý¿ã¿ß¿áØÚ÷¼à·Ü¥ç«‚V¹Å‡¿‡ýÊ¥Žì’H¿æ’¹¾ò“‡–F–ö—ü›{Ÿ\ª@¯‰³L³‚¶s·”½fÃdÑFÑÚÚœ¿çß õpýJ"},
    new string[]{"Kua","¿ç¿å¿æ¿ä¿èÙ¨†EŠ¯m•v—ë½\Å~ÈAÊyÐŽÕFã’ä˜ï¾îóg÷Á"},
    new string[]{"Kuai","¿ì¿é¿ê»á¿ëßàØáä«Û¦áöëÚ¿þƒ~„S‡ˆˆQ‰K‰‘ŽwX“ù”÷•þÒªœ­g¼[Ä’ÝÞÊ‰à”ñiôU¿ý÷d÷Ž"},
    new string[]{"Kuan","¿í¿î÷ÅÍêŒˆŒ’—p¿ÃšEšL¸T¸Uèwîw¿Åóy"},
    new string[]{"Kuang","¿ó¿ð¿ñ¿ò¿ö¿õ¿ï¿ôÚ¿Ú÷æþÞÅÚ²ÛÛêÜßÑOƒ—ÐÖ„Á…N…j‰¿DVûb‘Ç‘È’[•p•çÍ÷›r›¬äÒ p ï±q²Ž³m³q»ÇµV·ƒ¹n½T½_ÀkÕEÕNÙLÜ’ÜœÝAÝHÞ‚¹äßà—ãkäqèkù\üY"},
    new string[]{"Kui","¿÷À¢¿ü¿ú¿û¿ýÀ¡¿ø¿þ¿ùØÑã´Þñî¥õÍñùóñà­åÓêÒÝÞã¦à°Ø¸òñÚóÙç„l…T…t‡]‹‹ÅŒºŽhŽu‘|‘è•u—ó—õ˜æ™œšCšwš•¢Ÿ²z´j¸Qºˆ»AÈ±ÂÂ‘Â˜ÃvÄCÄ„ô§ÉJÊ‰ËwÌlÌwÌ€ÌõûÖd²ÈÛ“ÜiàkåžæKçqè^é êNã×í•í–íŸî`îÇêðrðòjóYÀ£"},
    new string[]{"Kun","À¦À§À¥À¤öïï¿÷Õçûõ«ãÍã§ÂÑˆÒˆÜ‰×‰Ú‹GŒ±Š‹•‚—y›Ù»ì„ŸjŸãª^­@±—³µŒ¶‘¶Ÿ½™ÁHÅCÇÎJÑTÑXÑhÑ‚ØcØ~åKé€éîBÍçðQâÆòOó‚óˆöHöŠ÷¤ù{úAûdýlö¸"},
    new string[]{"Kuo","À«À©ÀªÊÊòÒèé»áØÚßà‡p‡ˆ‘²’ˆ’•”U•þ—ItžN T ‰¶„¹QÈuÈvÚ÷à—éŸìHíAíTípîSó–ôUÀ¨"},
    new string[]{"Kweok","·i"},
    new string[]{"Kwi","™Í"},
    new string[]{"La","À­À²À±À¯À°À®À¬À¶ÂäðøååíÇØÝê¹ƒ•‡Ä“X“Y“yß¡”Y”j–¬—ï™Êœ¼ m­†°]´rÁÄ—ÅDÇ‰ËˆÎ`Î|ÏžÞhènéJíBôFö_÷vñ®"},
    new string[]{"Lai","À´ÀµÀ³äþêãáÁäµïªô¥áâíùí‚g‚|„Ð…–†‹ŠÅ‹@ˆŽòÆ‘Ð”j—…—®™ÊœZž|žª[¬[°]²A¹X¹s»[ÈRÌDÒsÕvÙlÙ‡ßFà[áånîmîsòQöDù`ù„üH÷óñ®"},
    new string[]{"Lan","À¶À¼ÀÃÀ¹ÀºÀÁÀ¸À¿ÀÂÀÄÀ»À¾À·À½ÀÀé­á°ñÜïçìµî½äíƒNƒ‹…•ßø‡•‡Ûˆh‰°‰·‹ö‹ûŒG¹ŽÓ[âÞ°ãÁ‘¾‘Ð”G”r”ˆ”Ì•©™Ú™ì™í›ÇÁ°œ‹ižEž‘ž™ž°ž±Á¶Ÿ’ A L f € ˆ Š­s­Šµf»@»_¼hÀaÀ|ÈŸË{ÌkÌmÒ[ÒhÒwÒ€ÓEÓ[ÖG×E×ŽÚÉÜ_³»áYè|è”ê@íeïC"},
    new string[]{"Lang","ÀËÀÇÀÈÀÉÀÊÀÆÀÅïüòëÝ¹à¥ï¶ãÏÝõ‚Z„É†]†}ˆ°‰i‹™~”–J–T—O˜¸˜ÑšDŸR¬˜³„¹^Á}¸þÃžÅ…ÉvÉ‡Í™ÕLõÔÜqàHàOäZæƒéò@"},
    new string[]{"Lao","ÀÏÀÌÀÎÀÍÀÓÀÔÂäÀÑÀÒÂçÀÐÁÊñìï©õ²îîßëèááÀðìƒXÁÅ„º„Ú†K†[†ë‡E‡Z‹ª÷`‘Ž‘“ÆÁÃ–U˜÷™Q›Ð³ªJâ²«™°A³z´‹·ºŒ»”½jÂgÇNÞ¤ÍŒÏoÜ~ÞLã™ç„î‘ó€õu"},
    new string[]{"Le","ÁËÀÖÀÕÀß÷¦Øìß·ãîàÏà’A˜S˜·ší ¬«W³i¸…º{ÆIêbí‰ðEð›ö˜"},
    new string[]{"Lei","ÀàÀÛÀáÀ×ÀÝÀÕÀÞÀÙÀßÀØÀÜÀÚçÐÚ³ñçõªÙúæÐéÛàÏ‚ñƒ±Â¬…ŸßÖ‰C‰¾‰Í”b˜Ã™¦™§™ï›¤›æœIäðž˜­z®š¯°N±R²´´ µWµXµˆ¶a½t¿wÀhÀnÀ}ÀœÃšÄBÉ ÊuË‰ÌqÌrÌ{Ì…ÏœÕC×|Þ[à[ãåGèDèhèˆìYîLî[îïK÷mûPýF"},
    new string[]{"Li","ÀïÀëÁ¦Á¢ÀîÀýÁ¨ÀíÀûÀæÀåÀñÀúÀöÀôÀùÀìÀòÀüÀóÀþÁ¡ÀêÁ£Á¤Á¥ÀõÁ§ÀðÀ÷ÀøÀçÀèÀéÛªð¿óÒÛÞÜÂ÷¯çÊõÈòÛï®ã¦å¢ôÏÝñèÀó»Øªß¿íÂæËóöðÝðßáûÞ¼äàöâìåèÝà¦õ·éöîºæêî¾åÎÙ³à¬ö¨÷óÝ°ÙµòÃæ²ØìÎ»ƒ¢ƒ«ƒú„^ÁÐ„{„˜„°„î……“…–…«…¬ß·†o‡­‡³‡Î‡Ñ‰W‰ÈŠÚ‹KŒCŒVŒÞŒübqŽ_c“—Ÿü’A’FÞæ“…”^”i”ƒ”‰”Á•·•Ñ•å–^–ª–Ð–Û–ï—~—ˆ—˜˜»™‚™ª™µ™À™æ™ðšsšvšÓ›l›mÆü›É›ãœIžTžWžrž¢ž¦ i s À Ó Ø«†çó¬P¬—­|­€­‰­–°O°[°±L±X²@²—³PíÇíÑ´•µZµ[µ`¶Y¶]¶w·ˆ¸{¹]»h»Œ»š¼H¼c¾F¿rÀfÀ{ôçÃšÅƒÆnÆÇVÇ—ÉTÉWÊkËžÌyÍjÍÎGÎgÎ€Ï[Ï~Ï‹Ï ÐGÑYÑeÑŸÓ€Ö‚×ØNØ‚Ú\ÜVÞ]Þ^ß†ßŠáBárá‡áãWã‰ä‚ä‡äœåGækç\ègèpîåïÓë_ë`ëxö²ìZìcïSìªóPóœôfõŽõ”öP÷k÷u÷w÷~øEøtùvúbûZûû•"},
    new string[]{"Lia","Á©‚z"},
    new string[]{"Lian","Á¬ÁªÁ·Á«ÁµÁ³Á¶Á´Á²Á¯Á®Á±Á­Á°ó¹çöéçÝüöãÞÆäòì¡ñÍå¥ñÏé¬ŽÁîƒI„ …U…V†ö‡tˆäŠYæ®‹t‹¼‹ÕŒD‘X‘z‘ÙÂÎÞö“¢“ì””¿—†˜˜™¹™Úšašš›Ëœ‹œÇiÔïž‡Ÿ’ŸÈ R¬…­Iî¬´n·SºŸ»^»d¾š¿€À~Á„ÙúÂIÂŽÂÂ’Â“ì¢Ä˜ÜßÝ²ÈjÉËOËWÌ_Ì`Î‹ÑžÒcÒœÖ‹×`ÛšßBà˜ázåbå€æ`æœç ïçÁãôHönö–÷H"},
    new string[]{"Liang","Á½ÁÁÁ¾Á¹Á¸ÁºÁ¿Á¼ÁÀÁÂÁ©Á»Ü®ö¦õÔé£÷ËÝ¹I‚Z‚z‚ŠƒÉ†]†|†¤†È”¾ª’ë˜Å›öœ´Ÿ´º|¼Z¾H¾nÃžÍ™ÎWÑoÕõçÛ˜ÝgÝvÝˆÞcåyéãÏìnò@ôuÙû"},
    new string[]{"Liao","ÁËÁÏÁÃÁÄÁÌÁÆÁÎÁÇÁÉÁÅÁÈÁÍÁÊîÉÞ¤ÞÍå¼çÔâ²ðÓàÚÀÐƒJÀÍ„Ú‹»Œ®Œ³Œ×Ûùú\‘l‘’“š”¶•Å˜ÍxžÒ r v­V¯Ÿ²t¸N¸Xºƒ¿ÄkÄ‚ÏYÏiÏoØIÙ’ÛŽÜGÞLß|à€á‘çBç‚éHéRïfïmósú"},
    new string[]{"Lie","ÁÐÁÑÁÔÁÓÁÒßÖÛøÞæ÷àôóõñÙýä£Àý‚|ƒ•„µ„Ã†`ˆ´ŠGŠ²hŽ_Ž{è’ž’£”YÀõ—˜™§š¸›¼ŸIŸ­ M i m Úªd«C±Ÿ¾FÂ~Ã‡²²ÅDÆ”Í}Þ˜ååïVôQõh÷vø•"},
    new string[]{"Lin","ÁÖÁÙÁÜÁÚÁ×ÁÛÁÞÁßÁàÁÕÁØÁÝåàá×ÝþôÔ÷ëõïê¥âÞãÁî¬éÝì¢ßøzÈÎ²ƒj„C…›‰É…[t‡°‘¬“Ô”Ý•—•É˜ð™_›ØÉøB«ÅžŠŸiŸûª«l­U®V®ž¯r°R°S´@·A¹ƒ»‘¿šÂLÅRÈHÌAÙUÜCÜ\ÜkÞOÞ`àçléŠÒõëOïCò•õC÷[û‹"},
    new string[]{"Ling","ÁíÁîÁìÁãÁåÁáÁéÁëÁäÁèÁêÁâÁæÁçÀâôáòÈÜßç±ê²Û¹ßÊãöèùèÚöìñöàò‚’Àä„cˆ{‰çŠ–Šê‹øH’ŽX¶Á¯Áà’è•`–E™Ð™ôœRÎžƒžâ U ‹ ÷¬O°s³gÁ×µ’¶{¸n¸ ½@¾cÅzÉˆÊCÊ™ÌhÐeÐ‡ÑkÔfÚšÝCÝsáá”âä™¶¤éqêtë‘ë™ëëžì_ì`îIñ|õCöNøoû_ûwû™ýgýhý’"},
    new string[]{"Liu","ÁùÁ÷ÁôÁõÁøÁïÁòÁöÁñÁðÁóÂµÂ½ç¸ï³öÌïÖä¯æòì¼ðÒìÖåÞÙÍƒE„¢‡®‰g‹ˆ‹ôÍA‘ËÂÕ”å”é–Î—B—P˜ñ™P›fã÷±ÃÓÎx¸žgŸÞ«€¬Š¬–­]®o®q®‘®œ°@´e´z¾^ÁSÁ[Á’ÁÄÄ|ÝäÉ]ÉsÞ¤ÊVË˜ÏYÑ^Û‰ãTäæyçBçsÃ­éHê‘ëwìCïdïfïiïvðsñ‡ñœòtò˜ôjö†úVúwûmûˆ"},
    new string[]{"Lo","¿©‡Þ"},
    new string[]{"Long","ÁúÂ£ÁýÁûÂ¡Â¢ÅªÁüÁþÂ¤ÛâëÊççÜ×ãñèÐñªíÃƒ¥…€†U‡µ‰Å‰Æ³èŒ™ŒâðŽaŽbÅÓÜ×Ü”n•o•î–V—Y˜™™Éœ¬œöVž{ z­‡±€²”³Šµaµb¸_¸oºTº\»\Ã@Ê”ÌdÎgÐFÐHÐiÒtØFØLÚLÜ[çXèxë]ìNì_óGûTýˆý‹ýýŽ"},
    new string[]{"Lou","Â¥Â§Â©ÂªÂ¶Â¦Â¨ÙÍò÷ïÎÝäñï÷Ãà¶ðüáÐƒE‡D‰vŠäŒŠŒÍâI‘f“§˜Çœ¾UŸÓÀÎ®R¯›¯œ²k¸MºtÂeÄ|Å”ÊVÏNÖŒÜ}ßsçUíVót"},
    new string[]{"Lu","Â·Â¶Â¼Â¹Â½Â¯Â¬Â³Â±Â«Â­Â®ÂµÂ°ÂÌÂ²Â¸Â¾ÂºÂ»Â´ÁùöÔèÓäËÞ¤åÖãòéñëªóüéÖéûÛäëÍààïåê¤äõß£è´ðµðØôµôƒJ„—„Î„Û†ë‡£‡´ˆPˆv‰n‰o‰ÀŠáVL] ’ ÞÛ’ÇÞó“¦“ï”]”d”m–›—¶˜Ä˜Ì™©™¾šÚœGœOÂËFUÂžVžZžoŸÑ t«G«S¬f­o­ˆ®fñ¬±J±R²F²’³t³”´{µ“¶˜·c¹‚ºŽº—º˜»U»V»œ¾vÀrÀžÁ’·ôÄrÄwÄyÅFÅyÅ›Æ@ÆAÇŠÉÊIÊ€ÌJÌÌ”ÏFÐB½ÇÓtÓ€¹ÈÙTÚ€ÛjÛÝ`ÞAÞ_à~ÀÒáXâ„ä›åhåjæ”çGçeçœèuèzê‘ïBòJòƒózô”ô—öI÷w÷|øšùcùnú˜ûRûuüu"},
    new string[]{"Luan","ÂÒÂÑÂÐÂÍÂÏÂÎèïöÇÙõæ®ð½vyˆJˆKŠaŒDŒ\Žn”•ð™èž¤ž´°f°gÁcÃ‡ÅLÅMËHÌ‰ÓTá›èŽùFû["},
    new string[]{"Lue","ÂÔÂÓï²„…ˆGŒœ”^ÂÊ®ˆ·DÒ©ËŽÔ›äsäx"},
    new string[]{"Lun","ÂÛÂÖÂÕÂ×ÂÙÂØÂÚàðö‚‡÷ˆÀ‹E‘¥’à—‹œSœÓ±š´K´ˆ¶—¾]ÂbÄ@Ç’ÎFÕ“ÛiÝ†ä—êöM"},
    new string[]{"Luo","ÂäÂÞÂàÂãÂâÀÓÂáÂÝÂÜÂåÂæÂßÂç¿©ÜýäðÙùöÃÙÀíÑé¡ÞÛëáñ§ÞûãøçóïÝâ¤ÀÖƒ¬ƒ±„s†ª‡ÓR’µ’Ó”m”{”‰•ï¹û¸ñ˜·éÒ™µ™å™ïžTË¸ q Îâ£«M°e°³`³ŠÂµµ[¹J»j½jÀzÁ_ÄTÅIÆŒÉzÌ}ÍxòäÎó»ñËÓTÓZÔ›õÈÂ·ÜVÜsß‰ãtæƒæ èŒ¸õîbð”ñ˜ò…òŸõi÷wùBúŸ"},
    new string[]{"Lv","ÂÌÂÊÂÁÂ¿ÂÃÂÅÂËÂÀÂÉÂÈÂÆÂÂÂÇÂÄÙÍëöéµãÌÞÛñÚïù‚Hƒ–„í…iÂ¬…Î†`ˆ‡‰¾Â¦ŠäŒŠŒœŒÒ¯Â®]‘]‘f•ì—o˜Ç™¬™°™¾šÑŸf l¯œ±R²kµ~·t·„¹˜Â¨ºtÀÛ½…¾G¾v¿|¿†ÄoÄ|È„ÝäÊVËƒÒ@ÖŒÜ}àLäXåhçUèrïÎé‚ñeóHô”Â³úyÂ¹"},
    new string[]{"M","ß¼…Þ‡`"},
    new string[]{"Ma","ÂðÂèÂíÂïÂéÂîÄ¨ÂëÂêÂìÄ¦ßéó¡áïæÖè¿Ã´}‚ØÄÅ†á‡O‹Œ‹°‹ßŒIŒ­˜qœÔ Ðªw¬”¯q²K´aµTµl¶MÁRÝëÊhÎ›ÏWºÑõößjæ‹éUÃÒñRñˆò‡ôKö‡úiüN÷á"},
    new string[]{"Mai","ÂòÂôÂõÂñÂóÂöÛ½ö²Ý¤Ï„êßäÁ¨‡X‰ÓÅÉÃ}Ê{ËhÐ]Ø‚ÙIÙuß~ì@ìAú”ûœßé"},
    new string[]{"Man","ÂúÂýÂ÷ÂþÂùÂûÂüÂøÂñÃ¡á£÷©Ü¬òýïÜò©÷´çÏì×ƒKŠ›‹ ŒÌÄ»Ž‘`“¶˜´˜ÑœºMÏÙªƒ±”²m²–½ƒ¿zÊAÌpÏTÏ\ÐUÒZÖ™õçÛ˜à„æžçNî”ðzôMôNö "},
    new string[]{"Mang","Ã¦Ã¢Ã¤Ã§Ã£Ã¥íËÚøòþäÝ………¹†W‰ÜŠÁŒ´Ží}–Mëü–n–xšû›À ¯ ½ªK®m¯g±ZÞ«³‰¸ˆÆŸÇƒÌMÍ{Ï‘âIä€èšñ ûLûsýˆýÁú"},
    new string[]{"Mao","Ã«Ã°Ã±Ã¨Ã¬Ã®Ã²Ã¯Ã³Ã­ÃªÃ©ë£Üâè£òú÷Öí®êÄêóî¦á¹Ùóó±ì¸ã÷Ù°ƒÐƒÓƒØÛÃ„Õˆé‰î‹u¶Ø‘ùÃè•§–‰—ûš»šÊšÓœ~Ä² Óª…°p±g¶m¹F¾ˆÁEºÄÆdÈrÉ‹òÖÎcÒ‘Ø~ØˆÙQÜšà|àŽáFãTãwå^ìWóùš"},
    new string[]{"Me","Ã´‡¡‡ª‡¼ŒPŽÛžQ°ZüN÷á"},
    new string[]{"Mei","Ã»Ã¿ÃºÃ¾ÃÀÃ¸ÃÃÃ¶Ã¹ÃµÃ¼Ã·ÃÂÃÁÃ½ÃÓÃÄÃÕÄ­áÒâ­ñÇäØä¼ðÌÝ®÷ÈïÑé¹ƒñ…ÐÎ¶‡ªˆbÛéˆõ‰rÄ«‹Z‹‰‹Ê±Û’{’¯”u”|Ä³–Ï˜M˜Ž™­š°šî›]›iœ„œŽœÕŸ¢ B¬C¬s¯c±g±t±Œ²S²‚µ|¶C¹ŸÁoÃzÃŠÄPÄŠÆ€ÉBÌjÎnÚ›ÜzàdäYæVæ[íiômúBüeüq"},
    new string[]{"Men","ÃÅÃÇÃÆí¯ÞÑîÍìË‚ƒBŽž¸‘¿’Ð•¹—Èš‰ãëœºÂúM Fçä«f«j­J²m·`Ç–ÌŠå{éTéY÷´"},
    new string[]{"Meng","ÃÍÃÎÃÉÃÌÃÏÃËÃÊÃÈÃ¥íæòìÛÂãÂÝùó·òµëüô»ô¿Þ«ƒƒá‰ô‰õ‹“Œ´ŽÌŽí‘¸‘º’ú”BÃ÷•ä˜ýšÙœÉ÷«B®H®mî¨²‰²“ÁEÇmÈ_ÊpÎ{ó±òþà‘à–äYåië‰ëœìDìFìWìXîŸðóõ’öQ÷jûLûsüwö¼ü€"},
    new string[]{"Meo","Û_"},
    new string[]{"Mi","Ã×ÃÜÃÔÃÐÃÛÃÕÃÙÃØÃÖÃÝÃÒÃÓÃÚÃÑÞÂ÷ãßäãè÷çìòâ¨åôÚ×ØÂëßåµôÍà×ôéñÚ¢ƒßƒç„¯†O‰QŒBŒsŒ©Œª¶ûaŽ¶ŽÈçÛ›‘ÛÄ¦“º”C”V”}˜a˜Æ™™—›^›m›¦œPœ}äéDeðóž…ž§ŸÇ † –â´«J­Œ±~±‰²[²yµz¶[·`º€»H»…ÁAÁ]ÁdÆƒÈŽÉoÉqÊUÊZËzÎ^ÑAÒ’Ò“ÒšÔ™ÖiÖk±Ùá‚áƒáˆãèféSûJû†û”ü†"},
    new string[]{"Mian","ÃæÃÞÃâÃàÃßÃåÃãÃáÃäëïäÏííãæö¼äÅD‚a‚ÁÚ¤„Ò…›†»Šå‹i‹îÒ™†™¡šóãýœ¡Æ ¤î¨²Š²Œ²¼E½ƒ¾d¾r¾‚¾‡¾’çÅÅXÆPÇ|ÈxÎeÏŸìrìtõ|û ü@üIüMüwå²"},
    new string[]{"Miao","ÃëÃçÃíÃîÃèÃéÃêÃìíðçÑç¿íµß÷èÂðÅåã£³³‹b‹·ŽøR®Ã¨«Q¸kºF¼†¾ˆ¾˜É´òçÔN÷]ù‘"},
    new string[]{"Mie","ÃðÃïßãóúóºØ¿…¸ßä†_ŒPŽÏ‘Ì“}™­œçžf±uËIÐ`Ò”ÚÅèf÷xøp"},
    new string[]{"Min","ÃñÃòÃôÃöÃóÃõçëíªçÅãÉçäÜåãýö¼÷ªáºƒoƒäƒí„b„Ç…Ýˆ„Š“‰ëB‘O‘‘’Ï”•”°•F•G•¡ãëœbœ“œ¡˜¬Y¬\¬z¯x±]±aÃß³R´C¸œ¹Iº‡¾r¾‡ÀKÉþÁFÏŸÙ‚âŒä æFéhé}öšøsüw"},
    new string[]{"Ming","ÃûÃ÷ÃüÃùÃúÃøÃËÚ¤î¨êÔÜøäéõ¤âƒüŠ±‹“‘D’ø–L˜i›³ªu±b±…ÃÈÉqÓKÔšàpã‘øQ"},
    new string[]{"Miu","ÃýçÑ¿ŠÖ‡"},
    new string[]{"Mo","ÃþÄ¥Ä¨Ä©Ä¤Ä«Ã»ÄªÄ¬Ä§Ä£Ä¦Ä¡Ä®Ä°Ä¢ÂöÄ­ÍòÎÞÃ°Ä¯ï÷ñ¢éâïÒæÆÚÓÝëõöõø÷áÜÔâÉñòÃ´²®°Û„¯„¹Îð†ù‡±‡¶ˆ\‰sŠ‹‹ºŒ­Ž’ÅÁŽ”\‘½‘Û¸§“á”V”}ÃÁ•b•½–£˜íšzš{›]žfŸo jªC°Ù°t±u±‰±‹²a²h³]µc»Š¼U½Q½]¿}ÀgÅÇeÃêËÍˆó¡Ï_ÍàÑJÖƒÖ„×OØ{Ø€ºÑÃ²ã€æŸì…íHðxð‘ò‡órôŽôžüNüOüaºÙæÖ"},
    new string[]{"Mou","Ä³Ä±Ä²íøòÖöÊÙ°çÑßè¼þ„ÀÛÌ…ÞˆéæÄc”––üÎã›£²y¿ŠÏwÙóÖ\ãwíJøœüEòú"},
    new string[]{"Mu","Ä¾Ä¸Ä¶Ä»Ä¿Ä¹ÄÁÄ²Ä£ÄÂÄºÄµÄ´Ä¼Ä½ÄÀÄ·ÀÑîâë¤ÛéãåØïÜÙ„L‰ŠÃæÄ\Ž¿‘H–]—ú˜Òš»šÒžÑ ¸ ñª…®r®y®€®®Ž³c¿}¿ŠçÑÃkÅÆŸÇ€ÈrÍ]Û[ãaãfë‚ëŽíJß¼ºÙ"},
    new string[]{"Myeo","”æ"},
    new string[]{"Myeon","C"},
    new string[]{"Myeong","—Ò"},
    new string[]{"N","àÅ†Hßç"},
    new string[]{"Na","ÄÇÄÃÄÄÄÉÄÆÄÈÄÅÄÏñÄÞàïÕëÇpƒÈÄÚ…ÈßÎ†òŠ{’f’‚’œV¶g¸—¸™óèºO¼{ÐõÉSÉiÐœÔFÔiÕyØvØyÛÜ˜àGâcæ“ë~ì„ô›"},
    new string[]{"Nai","ÄËÄÍÄÌÄÎÄÊÄÄÝÁÜµèÍØ¾Ù¦‚™Š…‹èŒYi’í“ˆ“¯œ‡ŸÃ¯GÂYÄÜÄGÎ—Ñ”Þ•áår"},
    new string[]{"Nan","ÄÑÄÏÄÐôöàïòïéªà«ëî‚OàîŠ{ŠÉ‹R‹©m‘Ú’o“DÌ¯”‚•¨–––¹œ¯Ì²ž©Ÿ²®~Ç~ÈlÖQßaëyò¢"},
    new string[]{"Nang","ÄÒâÎêÙàìß­eƒ²ßæ‡‡°~‘“r“î™ò›ïžž²ÌZÐL×að–ôTýQ"},
    new string[]{"Nao","ÄÖÄÔÄÕÄÓÄ×Ø«îóè§ÛñßÎòÍâ®íÐ…Dˆß‰ë‹C‹špŽHŽjŽuF˜À‘“Ï˜ï™`½½²«D«L´L´ZÃ—ÄQÄXÄžÎjÏuÔi×Dçtémô["},
    new string[]{"Ne","ÄØÄÄÄÇÄÅÚ«…È’fðÛ±„ÔGðÚ"},
    new string[]{"Nei","ÄÚÄÄÄÙÄÇƒÈŠÌŠñšß›ÔÃ•ÄFåMðHð]õƒõ"},
    new string[]{"Nem","Ÿˆ"},
    new string[]{"Nen","ÄÛí¥‹\‹¯èÄÄQÄž"},
    new string[]{"Neng","ÄÜ¸o¶øÄÍÎ—"},
    new string[]{"Neus","Ç‚"},
    new string[]{"Ng","àÅ"},
    new string[]{"Ngag","â…"},
    new string[]{"Ngai","äG"},
    new string[]{"Ngam","†«"},
    new string[]{"Ni","ÄãÄàÄâÄåÄæÄØÄçÄßÄáÄäÄÝÄÞîêêÇÛèìòâ¥Ù£âõöòíþì»ÃƒŒƒ“ƒ¹ƒºˆÐˆÓŠ…Šö‹¤‹è‹òŒNŒTŒÉŒÛáÚ›©îí«‘¹’f’v’í”M••¿–«—´™šîœNäÜðôž…ž— ù¯[±z¶[¶v»u¿QÂžÃfÄQÄÅMÆsËoÍeÍ‰ÎUÑAÓrÕy×rØƒÛCÝrÞ‹à\â‰ãbèXéSëWñDöFûŒýu"},
    new string[]{"Nian","ÄêÄîÄíÄìÄéÄëÄèÕ³Ø¥ð¤éýöÓöóÛþ…`†P†ˆŠ¨’×“Ó”f˜^›ÝœVœÇ¯[¶j¶|ºvÅˆ³ÃÚfÚ™Û…ÛœÜTÝ‚ÝšÕ·õRöTùD"},
    new string[]{"Niao","ÄñÄòôÁÜàëåæÕ‹–‹ØÞÍŒ³˜ÒÄçÆ›Ê\ÑUÑ™øB"},
    new string[]{"Nie","ÄóÄøÄôÄõÄùÄ÷ÄöÚíÞÁà¿ô«õæò¨Ø¿Äß†Ç‡y‡§‡Ë‡Ù‡Üàïˆ[ÛþŒZT»ÔŽLŽqŽ‹ÐÒ¶Äí’í“I“”Éã“µ”z”¤–¨—´˜®™Ç¯[ºQ»H¼b¼fÂ™í±ÅYÆ}ÇŒÐAÒAÕ”×‘ÛWÛfÛhÜbãbãcäOäŽåRæ‡èXè‡èêEêŸïDým"},
    new string[]{"Nin","Äúí¥‡á’ŒÃ€"},
    new string[]{"Ning","Å¡ÄýÄþÄûÄüÅ¢Øúå¸ßÌñ÷‚Aƒ‘±ù‡“‹ÞŒ|Œ‚Œ„Œ‰ŒŽÄê”QÈÁ™F™ŽÃôªŸÒÉ²…ÂœÆrËfè_ôVôXûH"},
    new string[]{"Niu","Å£Å¤Å¦Å¥ÞÖæ¤áðâîF’j–ƒ›S›\žÈ «¼~ÇyòÊâoì"},
    new string[]{"Nong","ÅªÅ¨Å©Å§ßæÙ¯ƒzßÇ‡‘’˜’°™×â°J¶Z¶Œ·vÀYÄ“Ç_ÊÒaÞrÞsáxýPÞÃ"},
    new string[]{"Nou","ññ†Ž‹ç“x”J˜‰™“«AÁ…×a×kæeç"},
    new string[]{"Nu","Å­Å¬Å«æÛæÀæååó½ö¹Â‚ÕßÎàû“x”J³e¹@ÈìÔiñw"},
    new string[]{"Nuan","Å¯Šfœqœ¨å¦ŸœŸð`"},
    new string[]{"Nue","Å°Å±ÚÊ³–"},
    new string[]{"Nun","üQ"},
    new string[]{"Nung"," \"},
    new string[]{"Nuo","Å²ÅµÅ³Å´ÄÈßöÙÐï»Þùƒ®…ÈÄÅÄÄˆë‹s‹µÞ‘Âµô’ý“x“—j˜`™DšÃœx·L·z¼K¼XÂXÑDÑEÖZÛßSÄÇàGåŸÄÑëyÐè"},
    new string[]{"Nv","Å®ô¬îÏí¤–H›\áð»sÐõÂxëÇÐZâS"},
    new string[]{"Nve","‹FÅ±¯‘Å°"},
    new string[]{"O","Å¶à¸àÞ¹p"},
    new string[]{"Oes","‰ñ"},
    new string[]{"Ol","j"},
    new string[]{"On","•jíM"},
    new string[]{"Ou","Å¼Å»Å·ÅºÅ¸ÇøÅ½Å¹âæê±Ú©ñî…^…¾…Ë‡I‰p‘Y¿ÙÎÕ“¸”·™¯šWšªä×aÏŸà®T¼uæúÄUÄpÉ’ÊqËšÖŽÓöáqæ–økútý{"},
    new string[]{"Pa","ÅÂÅÀÅ¿Å¾°Ò°ÇÅÁÅÃÅÉóáèËÝâ°È°ÉŠrŽ°Ñ’öšñ°qÅu°ÅÆtÐ’Ú•âZîÙ"},
    new string[]{"Pai","ÅÉÅÅÅÄÅÆÆÈÅÇÅÈßßÙ½Ýå·È—“—À ÛªT¹uº’Æ¢ÄMÝ‡æW"},
    new string[]{"Pak","´s"},
    new string[]{"Pan","ÅÌÅÎÅÐÅÊÅÏÅËÅÑÅÍ·¬°ãÅÖñáó´ñÈãúÞÕãÝõçZ°éƒë°ë±åˆmŠ™æ©‹ŠÉóŒqŒŽ´ÛÍÑå°â°è“„–®˜„›c›œ°œãžbžcžÎÆ¬ ž ¥®‰ð«±P±_±e±~´B´‘»O¿T·±Ä‡ÉgÎŒÑ—ÔjÛAÛsÛ˜Û¶äƒæoè‹é›íQîGùb"},
    new string[]{"Pang","ÅÔÅÖÅÕÅÓÅÒ°ò°õäèáÝåÌó¦·Â°ø…€†ç‰â‹˜Œ´ÅíÏ·¿·½›P›`žÐºUÃTÃpÄtÅ}ÝòÐIÓIÚ“·êæ^°÷ë„ìQóoö„÷›ý‰ý‹"},
    new string[]{"Pao","ÅÜÅ×ÅÚÅÝÅÙÅÛÅØáóÞËâÒðåëã°ü‡¥ˆƒŠE±§’“¿žä  Ü­”°’³hµPµ^·…·•°ûÃ‡°úÈaË‘ÍdÐˆÑŒÖcÝNãEè˜ìŽïRì©õU±«ûûƒüB"},
    new string[]{"Pei","ÅãÅäÅâÅÞÅßÅåÅàÅæÅáì·ïÂàúõ¬ö¬àÎ¬‚_±¶·È»µˆ¡åúŠvŠ³Šç‹fïC’yÞå”h”ä•^–È—“šÅ›ÖäÄ é«˜¬a¬i¸ŸÃSåõÜØÆžÉ„òãÐ[ÑpÙrÞ\äžêkêŠñ]ñs"},
    new string[]{"Pen","ÅçÅèäÔ·Ô…Ü†Ï‡Š±¾š\·Úå­›ÁÂMÈ†Ðv"},
    new string[]{"Peng","ÅöÅõÅïÅéÅîÅóÅíÅôÅëÅðÅòÅêÅìÅñâñÜ¡ó²àØºà‚‡°ø‚õ„™„ú‰X‰k×¯‹y‘u’²’ü“s“žÅÔ—Z—Ä—Õ˜¨˜Õ›€œAœKäèmpŸÔ¯n°v³y´y·@¸†ºU½lÀeÃgÆMÆ»ÇLÇlÏeÛsÝJÝZÝ~Ýƒ±Å·êßJåAèméoíŠíŽñsòuó—óŸôJùi"},
    new string[]{"Peol","›¹"},
    new string[]{"Phas","Ž‡"},
    new string[]{"Phdeng","êC"},
    new string[]{"Phoi","n"},
    new string[]{"Phos","†Ô"},
    new string[]{"Pi","ÅúÆ¤ÅûÆ¥Åü±ÙÅ÷Æ¨Æ¢Æ§Æ£Æ¦ÅùÅýÅþÆ¡Æ©Åø·ñõùØ§ÛÜæÇñ±Øòß¨Û¯ê¶èÁî¢òçÜ±ÚüßÁÚéîëâÏî¼Ûýç¢ÚðäÄàèò·ñÔ¶ÉÙÂ¸±±°‡‡›‡º»µÅà‰ªŠv‹œBšïàú±ÓâØW‘šÆË’y·÷“F”è–C–Š—À˜[±Èš³š·œk‡Ýå¨žÌŸ ò øªW¯@ñâðí¯w²D´iµFµG¶u¶y·K¸“¹vó÷ºfº”»z¼„ÁTÁ`Á‘Â\ÃYÃ˜ÄMÄmÜÅÆkÜÖÝÉÞ¬°öÍnÍoÎ“ÏKÐK±»Õ|ØuØwÛ¶âWâbâtââ”ãYãã›äšåCæqîÐêVêoëRí@îHîˆÆÄñyó‹ô“õBõQ÷‰øaùdúûG"},
    new string[]{"Pian","Æ¬ÆªÆ­Æ«±ã±âôæçÂêúæéëÝõäÚÒ‡æ‹xÆ½Ì—è˜Fªp­p¾œÄAÈqòùñÛÒÕ—Õ›ÙGÙXÛM±çÞqñ‰òNò]ò_ójôú@"},
    new string[]{"Piao","Æ±Æ®Æ¯Æ°ÆÓóªÝ³æÎî©éèçÎàÑæôØâƒG„Ü®Ò‘G“¿”ô‡ Ü°Ž´‚ºg¿~ÂH±ìÊEËiÖ€áoêQî’ïgïhòŠóQóTôw÷Ô"},
    new string[]{"Pie","Æ³Æ²ë­ÜÖØ¯‹±“Å•È‡·Î±ÎÒ”çv"},
    new string[]{"Pin","Æ·Æ¶Æ¸Æ´ÆµæÉé¯æ°êòò­Ø°‡¹æ³‹åÞÕ–Wšý±Ã«n¬V²‹³WµI·|ËdÌOØšîlïAñPóD"},
    new string[]{"Ping","Æ½Æ¾Æ¿ÆÀÆÁÆ¹Æ¼Æ»Æº·ëæ³öÒèÒÙ·‚‡„R…ç‰BŠÐŒÎJŽ—Ž£Ž±‘k‘{™q›€›¯›ÚœKŸv«r®J®j³fÅé³y¸z¹’ºqÀÂ†ÃgÆEÇLÉ‘ÌOÍgÍƒÔuÝZÝƒàZãuîZñTõGÚ¢³Ó"},
    new string[]{"Po","ÆÆÆÂÆÄÆÅÆÃÆÈ²´ÆÇÆÓ·±ÆÉóÍð«îÇÚéÛ¶ê·ØÏçêîÞ†\‡MŠUŠËŒžŒ ŒÛŒûFŽˆg“„”’•^—K—á™›¨œ_œ”œÂäßŠžTŸBªt°~³kÁ‘²²ÉbÊXÖcõËáNáwá•ãOçk°ÔîHñFñpómãø"},
    new string[]{"Pou","ÆÊÞåÙö…Ä…ð†Vˆ¡ÅàˆøŠç’g’h±§’½—”¸¢ Á¹rÑf°ýÒJõÛ²¿à^äžïÂïH"},
    new string[]{"Ppun","ƒÍ†R"},
    new string[]{"Pu","ÆËÆÌÆ×¸¬ÆÍÆÑÆÏÆÓÆÐÆÎÆÙÆÒÆÔÆÖ±¤ÆÕ±©ïèàÛÙéäßå§ë«õëè±ïäƒW„ƒ°þ²·‡þˆO‰Ž}Ž~·ö’p’Ã“ä“òê·•®–¿˜ã™kªžÊŸMªŽ¸¦¯j²r³h¶·o¹rÀbÅmÅnÜÞÇ[ÇŽÉhÍ—ÒLÒiÖE×VØfÙŸáTäçhç’ê†ñmõ‹ùLë¶ÆØ"},
    new string[]{"Q","è£"},
    new string[]{"Qi","ÆðÆäÆßÆøÆÚÆëÆ÷ÆÞÆïÆûÆåÆæÆÛÆáÆôÆÝÆâÆñÆöÆúÆüÆîÆàÆóÆòÆõÆçÆíÆÜÆèÆêÆé»üÆù¼©ÆãÆýÆìì÷ñýæëá¨áªõèÝ½Þ­èçí¬ÜÎÜùÝÂÜ»ãàØ½÷¢Ù¹éÊàÒòÓôëØÁì¥ç÷÷èçùòàÛßè½ÝÝíÓä¿ìóêÈç²Ø¢…¼¿‚ˆÙÊ‚úƒ[ÇÐ´Ì„~ÇÚ³ÔÖ¨…Ñ…æ…ý†u†ƒ†™†š†¢†Ð‡rˆÎ‰óŠÝŠíËÞŒóÃ¼ºŽ©æâåôâéjí¢¢Ôæ÷‘h‘i‘s‘¼’M’Q’W¼¼µÖ’†êü’Ý’å½Ò“ Ö§”Œ”ª”Å”Æ”ç•’•´–OÖ¦–Ö—R—t—‰—Ž—¤—«˜™‡™–™ûš©šÝšâ››¼ÃœDœg×ÕœjœŒœœënùúžÅŸdªX«O«^­D®P±Â»û¯O°ž±[³H³ž´J´\´m´w´ƒ´„µJÊ¾µo¶Q¶S¶¸gº‘º“»K»ž¼–½e¾L¾Nôì¾_¾e¾z¾ƒÀdÀŽÀ™ÃIÃXÄšÅpÅ ÆZÈWËjËsÌIÍTÍVÍ[ÎBÎ‰ÏBÏlÏ„Ï“Ð}ÑEÑvÑwÑzÓsÓ™ÕƒÖHÖ[ØMÚ|õÁÚ–ÛaÛeÛpÜeÜjÜ•Ü™Þ€¶ºßŒàVàœâHåWçKçˆèŸêMëBë’í ð‡òTòUò€ôGônôoôtôyõlõšö’÷’ùuù}ù†û˜üýRýt"},
    new string[]{"Qia","Ç¡¿¨ÆþÇ¢÷ÄñÊÝÖÒƒrƒîßÒˆX¿ÍŽ˜Ù’u’‰êü“U“Š“ü˜Hšðâ³L³s´l½eÚžáMì—õ^öÚ"},
    new string[]{"Qian","Ç°Ç®Ç§Ç£Ç³Ç©Ç·Ç¦Ç¶Ç¥Ç¨Ç¯Ç¬Ç´Ç«Ç±Ç¸ÏËÇ¤Ç²Ç­ÇµÇªá©îÔå½óéÞçåºÙ»ã»ã¥í©ò¯ÜÍÝ¡ç×ÙÝÜ·ÚäëÉÜçèýêùå¹q¤½‚]‚¡‚ßƒLƒŽÆàÛÉ„X…•†k†éˆTˆU‰q‰‰‰µŠdŠú‹`‹ì‹üŒRŒòÒQâãøŒ‘a’R’ƒ’Š’®’ç“B“b“¾“Ã“ËÞþ”o”p”q•ü–e–}¸Ì—˜p˜˜ ™N™Œ™¥™÷šKšMškšþ›Fä¹œDœ\½¥äÕu“žKžUžž£žèŸšŸÈŸï R ¿°|¸d½î¹ˆºGºRºž»R»`»x¾P¿yÀ`ôÇÁuÁ{ÃëìÄdÅOÅˆÆgÇ@ÇMÈ“ÈœÉ`ÊgÊnËÍOÍZÎSÕÖt×lØ@¸ÏÜÝ€ßwâTâ`âjã@ãQãUäEäuåDåXæPæZçcèBècè~ï·é_ëeìyíaîvñUò`òcòqôRôSöö‘÷œøZùkúYûeðÏübýlö¸"},
    new string[]{"Qiang","Ç¿Ç¹Ç½ÇÀÇ»ÇºÇ¼Ç¾½«òÞõÄê¨ñßãÞìÁïêïºïÏôÇéÉæÍ„“„ßßÑ†…†“†Ü†ó‰‚‰¦‹ÔŒ¢èÇìZŠ™‘c‘ê¿Ø“Œ“¬“°”Ö—¾˜Œ™{š£œÙ\ŸÍãÝ › ª]ª}«o¬j¬š³Móäº[¿‹ÀHÁmÁuÁzÁ†ÅšÊ@ËNÌbÓHÖmõ¼Û„Û–äæjçIçjî›úIû]"},
    new string[]{"Qiao","ÇÅÇÆÇÃÇÉÇÌÇÂ¿ÇÇÊÇËÇÄÇÎÇÏÈ¸ÇÇÇÈÇÍÇÁéÔÜñõÎíÍã¾ÚÛ÷³ã¸çØÚ½Øä‚¸ƒSƒsÏ÷„ä†Ì†×‡a‡„‰U‰Œ‰”‰§‹´á½ÏþŽŽ»ŽÉ³îÕÐÉÓ¸ã“³“êë¸Ð£˜“˜ò™]š¤š¨ë¥‰½¹Ÿ}ŸòŸ÷ Ö¯ ±³~Ïõ´`´x´“´™´¸G¸[¹›¿”ÀRÂNÜúÇJÇŸ½¶ÊwË–ÏfÕV×K×SÚˆÚ‰õÓÛXÛ^ÜEÜFÜNàbàzàƒà…õ´á ã“å æ@çDçyèAï¢ê~íIíXímîNî–î˜òœ½¾ófó|ó~"},
    new string[]{"Qie","ÇÐÇÒÇÓÇÔÇÑÆöÛ§ôòã«ïÆæªóæã»Ù¤êü‚Œ‚ž‚Í…L…‚ßþà©Æõæ¼‹}Ž¨‰Ü½Ý–A—¸Æã›­›ùÆá¯C°m·G·l¸`¸›ºD»]¾fÂëâÆjÞªË~Í„Í‰ÔˆÛBÛoå›çƒôŠö@ölöø"},
    new string[]{"Qin","Ç×ÇÙÇÖÇÚÇÜÇÞÇØÇÛÇßÇÝÇÕßÄñûñæôÀÜËäÚâÛàºòûàßÞìéÕï·ƒ¡……Â†wˆaˆ¨ˆ²ÝÀ‰ƒ‹]‹ŽŒ€Œ‹Œ˜ÂôûŽÜQø‘[‘¥‘¦’R’a’Í“l“å”Ü•T—véÈ™N™ÂšJ½þ›ØÉøBàäžp«¬l¯²›¸½ÂlÃQÅOÇ™Ç›ÈBÝèÞ­ÌCÌIÍZÏOÏˆñÆÓHÕWÚ_Úcâ\âdâsäuëdì€îMîzî›ò¢ñŸóVôgõøV"},
    new string[]{"Qing","ÇëÇáÇåÇàÇéÇçÇâÇãÇìÇæÇêÇ×ÇäÇèàõö¥éÑóäÜÜòß÷ôóÀöëíàôìÙ»ƒAƒ ƒõ„…„Í†¦ˆ½Éù‰ð‹]ŒxŽöF‘c’á“÷•¦—³˜½™”™¼š„š šä›Üœ[œ‚NžD«l®_³|³³ ¾«¾PÊ¤ÇmÈÕˆÝXÝpàWè[ìiìmí•õ›ù‚"},
    new string[]{"Qiong","ÇîÇíõ¼ñ·ÚöòËÜäöÆóÌƒ’…o‹ÖŒ^Ä‘w–÷™KŸwŸzŸ¦ŸÅ¬I­W­‚­Ž±ž²`¸F¸\¹HÅ|Ë}Ë•Í‹Ú^¾Ï"},
    new string[]{"Qiu","ÇóÇòÇïÇðÇö³ðÇñÇôÇõ¹êé±òÇôÃôÜòøÛÏåÏÙ´ò°êäöúáìäÐ÷üåÙHÇø…œ…´†pÍÅˆw‹pËÞŒx¦Ž€nã°ã¸’@’º“z–_—W™Ïš‚šÂÙÛšðšü›½œrœ©œªŸª ³«U­G°“±H¶k·hºE¼z½‡¾ÃFÜ´ÇiÈcÉ’ÌUÍAÍÎ~ÏbÐ@ÓaÓpÓˆÓ‰ÙgÚzÚ‚ÞÚþábáá–âUäMîÅíFíGØ¸õFõ‰öpöq÷A÷GøFùjù”ð¯ûjý”ý•"},
    new string[]{"Qu","È¥È¡ÇøÈ¢ÇþÇúÇ÷È¤ÇüÇýÇùÇûÈ£Ðçó½Þ¾ìîÞ¡íáÛ¾Ú°ð¶ãÖôðñ³áé÷ñè³ë¬êïòÐëÔöÄá«ÜÄÇÒÚ„`…J…Z…^…¾ä…íˆoŒþEç¾ÞlßI‘t‘ó’|”·”×™á™úšª›µœTž›Ÿa­S¸l¸y¹L»c»–¼ ½M½P×éÁ”ÂJÂ^ÃaÃlÃÅJÇ†ÈÍmÀ¯ÎƒÏJÏgÐRÐdÐ ÓNÓUÓYÔsÔxÕFÕoÚmÚzÚ…Ú ÛBÛRÜdÜ|Ý@Þ‘åáàTã^èLèŠé‰é˜êr¾Ï÷¶ñlñnò|òŒæãó”õ@õLö÷OøzùŠûYüCüDüLüzüšýxØÎ"},
    new string[]{"Quan","È«È¨È°È¦È­È®ÈªÈ¯È§È¬È©îýóÜç¹Ú¹éúî°÷ÜãªòéÜõç„á„ñ¾í†­‡üÛÚˆ»ŠºŠ÷ŒAZŽkŽ†ƒw³Ë©’Ô“‘–Õèð—¨—Ñ˜T˜¤˜Ø™à›L›§œ²žµžï º » Åâµ¬g¬†®l²•³o¼ƒ½h¾J¿X´¿ÄCÈ›Ì†ÐSÓjÔÖw×NÛIÛmÝbà ãŒçzêBíjïEñògöe÷™ûXðÙýjáë"},
    new string[]{"Que","È´È±È·È¸È³ÈµÈ²È¶ãÚã×í¨‚à¾ö…sˆ«‰U‰”Çü‚â‘U“nß«”¦š£š¨šõ›Q PÁÔª“°”³‚´F´_´`µCµ]ÅbôªÉÖÉUÍXÚ|ÛeÜeé êIëaøBùo"},
    new string[]{"Qun","ÈºÈ¹÷ååÒ‡ï‰æŒlnŽ šV¹„ÁtÑdÛZÝl¶ÝûŠûŽ"},
    new string[]{"Ra","’Á@"},
    new string[]{"Ram","‡Ý"},
    new string[]{"Ran","È¾È¼È»È½÷×ÜÛòÅƒÑ…m…ß‡YŠ˜‹v–¹™LŸß«z¿‘ÃVÅjÉGÍcÐ€Ð…Ð™Ûœó†"},
    new string[]{"Rang","ÈÃÈÂÈ¿ÈÁÈÀð¦ìüƒ¨„ð‰´‹úÝ‘Ó™Öž }«K·yÀvÌZÐL×j×ŒÜ`è‚ÏâôX"},
    new string[]{"Rao","ÈÄÈÆÈÅÜéèãæ¬‹ÆÄÓ“Ï”_˜ï á·n¿À@çÔÊÏuÒYßvëNðˆ"},
    new string[]{"Re","ÈÈÈôÈÇßöÙ¼’ÚœcŸáÛ"},
    new string[]{"Ren","ÈËÈÎÈÌÈÏÈÐÈÊÈÍÈÑÈÒÈÉâ¿éíØðÜóÝØñÅïþ¡¶ù„UŠžŒãáäí¥’P–Z–k–ß–á—e—ª›Ý ®¶e¶‰¼x¼Œ½V¾BÀÃMÄHÆ\ÇYÇŒÑGÓ•ÕJ×šÜrÜâJâmã…ìzì~ígïƒïšôøžØé"},
    new string[]{"Ri","ÈÕ‡ðšÞâJâ~ñ_óR"},
    new string[]{"Rong","ÈÝÈÞÈÚÈÜÈÛÈÙÈÖÈØÈßÈ×éÅáõáÉëÀòî‚Ô‚æˆc‹†‹’‹æŒ]tÊŽVŽc“m“r“–•í–Ñ˜s˜xš¿šÕžqŸV h¬Œ·Z·\½q¿^¿dçÈÁsÆŽÎÏ”Ñ’ÝPægéF¸ôížËÌñŒó“"},
    new string[]{"Rou","ÈâÈàÈáôÛõå÷·…œ‹YŒ`˜QœnŸ§¬y­~¶b»€Ä\ÇyÈ|ÎjÝŠåˆè`íqòkóökù’"},
    new string[]{"Ru","ÈçÈëÈêÈåÈãÈéÈìÈèÈäÈæÝêñàï¨àéçÈå¦Þ¸ò¬äáä²û‚¢…Ê†B†äÅ®‹‡‹çŽ]Žš’C’”J•ãÔÂ–d–ô™“œx ^«A¹T¿dÀ]ÈâÃNÄžÉSÊ‡ÑMÞzßàrá}ãœè`Ðèîž÷pøMønø›"},
    new string[]{"Rua","’µ"},
    new string[]{"Ruan","ÈíÈîëÃ‚¢ˆë‰¼‹\‹¯Þ“É™“œxå¦ ^¬}­w´MµO¾ÂXÄQÎpÜ›Ý‰Ðè"},
    new string[]{"Rui","ÈðÈïÈñî£ÜÇò¸èÄÞ¨ƒµƒ¶¶ÒƒÈÄÚ…±‰ÇŠñ»’f“É—M—‡™G›I®c¸½—¾qÀBËçÆÊtÌGÌHÎTÛbâcäJä„çiÄÆ"},
    new string[]{"Run","ÈóÈò“É˜ô™écét"},
    new string[]{"Ruo","ÈôÈõóèÙ¼…ª‹S×ÈÇ’µ’Ú“É—íœcÄçŸx kºOÜÇÉmàeö}ö”úU"},
    new string[]{"Sa","ÈöÈ÷ÈøêýØíØ¦ìªëÛ“—”c—Eéß™¨š¢›‡¥ž¢À{²ÌÊ”Ë_ÔQÜaâlæpçoè•ëMëìƒì‘ïSñ`"},
    new string[]{"Saeng","–Ó"},
    new string[]{"Sai","ÈûÈùÈúË¼Èüàçƒw†ð‡Tà“HšºšËºwº›Ùî|öw"},
    new string[]{"Sal","oÌƒ"},
    new string[]{"San","ÈýÉ¢É¡ÈþâÌôÖë§ö±‚^‚ã‚ð…x²Î…¢…£…¤‰ÐŽ¥q™VšÉšÐ¥ Ñ¼B¼R¼V¼W¿™ÊQÖçDçoédð€ôLáêãß"},
    new string[]{"Sang","É£É¥É¤òªíßÞú†Ê–ø˜šÀvÑ˜ærî‹"},
    new string[]{"Sao","É¨É©É¦É§ÉÒÜ£öþëýçÒðþÉÚý‘¨’ß’û™]š×œÐŸ¯Ôï²„¿„¿‰ÀRçØà“èAïbòXò}óöYö…öŸ÷f÷“"},
    new string[]{"Se","É«É¬ÉªÈûØÄï¤ð£†ÝZå‘­’‘“ö–ÜéÊšmšoÆü››œi×Õœàn­®æíži¬X­i¯™·w·†»ÀNÇ¾ËNÌŸÖ ÞQãGäCæaæ|çmîéï¡êSëïo"},
    new string[]{"Sed","ÑS"},
    new string[]{"Sei","›ØÂ{"},
    new string[]{"Sen","É­²ô“½˜¦ÉøBºdÒI"},
    new string[]{"Seng","É®ôO"},
    new string[]{"Seo","é~"},
    new string[]{"Seon","¿L"},
    new string[]{"Sha","É±É³É¶É´ÉµÉ°É²É¯ÏÃÉ·É¼àÄßþöèö®ï¡ððôÄêýì¦o‚ƒƒ„x†~†—†ÃÒ­Ž¨B’­½Ó“”Éã“—”z˜f˜×š¢çªQ³¹€»}¼†¿À\ÁœÁ ÇÈSÊeÙdÙhÉÞæ|é„éŒëô‹õõ"},
    new string[]{"Shai","É¹É¸É«õ§“—”ƒ•ñš¢ºYºkº»iÀ\ÐgÖLéŒ"},
    new string[]{"Shan","É½ÉÁÉÀÉÆÉÈÉ¼É¾É¿µ¥Éº²ôÉÄÕ¤É»µ§ÉÅÉÂÉÇÉÃÉÉæÓóµÜÏìøõÇÛ·äú÷­æ©ØßæóðÞëþÚ¨îÌô®Ûï‚ÞƒRƒdƒ{ÙÙ„h„š…g†Î‡AˆZ‰‰Ž‰¯Š™ŽEŽ»’´’ï²ó““½“ú”v”»••Ú•Ü–u–Å—Ö˜èÌ´™c™Ò ¿å£ž¨žèŸSŸšŸÄªGªk¯Z±˜´Š¶U·_¸–¸ž¿„¿˜ÀuÁÁƒÃˆÈÊ`ÏsÏ€ÒIÒvÓ@Ó˜Öb×iÙ Ú]Ü‘µËßáŸãˆç—éWéXé^ê„îtî²üðƒò~õŠ÷W÷X÷gø@áêÛÉ÷Ô"},
    new string[]{"Shang","ÉÏÉËÉÐÉÌÉÍÉÎÉÊÌÀÉÑìØõüç´éäÛðA ‚û³¡ˆÃˆö‰jŒ¬vÕ‘^‘ûš‘œ«Cg¶@¾yÊKÏDÐLÓxÖ…ÙpÛ}èlì ôl"},
    new string[]{"Shao","ÉÙÉÕÉÓÉÚÉ×ÉÒÉÔÉÛÉØÉÜÉÖÕÙÇÊÜæÛ¿äûô¹òÙóâÔÏ÷…pŠ¾„ÕÐËÑ”ï–¶äÑŸ†Ÿý d«x±óÔ½B½‹¾Kç¯ÇzÈVÈpÊ–ÐŒÝiíIímïYó™õ}è¼"},
    new string[]{"She","ÉçÉäÉßÉèÉàÉãÉáÕÛÉæÉÞÉâÉåÉÝì¨ØÇî´â¦÷êäÜÙÜ…‡ŠL‘b‘Ø’wÊ°’¡’ÎÞé“”“º”z™™Ý›õœhž—®Œ½Þµú´’ÄôÂ™ÅhÈ~ÊJÍFòÒÍ…Ï‡ÔOÙdÙhÝfêAê^íHísòM"},
    new string[]{"Shen","ÉíÉìÉîÉôÉñÉõÉøÉöÉóÉêÉòÉðÉë²ÎÉéÊ²ÉïÉ÷ÝØôÖÝ·Ú·ÚÅïòé©äÉò×ßÓëÏ·ê‚LƒÂ…¢…£…¤ßÅ‡AˆÞŠŠ·‹Ž‹ðŒJŒqŒŒævŽ»zõ’J’bÞÓ“•Y•Ö–¸—ª—Ø˜Y˜¦šá›ØœVBžcŸö«|®`®e¯}¯”±m±s²_²s² µŠ·Œ»p»r¼¾DÁAÁKÃŒÄIÈÉ†ÊQËMÍ–Ñ[Ó\ÔBÔYÔ–Õ”×}×Ÿß•ãhävÕðîTñ‘ôõŠõ˜öYö•öŸ÷“ù_ül"},
    new string[]{"Sheng","ÉùÊ¡Ê£ÉúÉýÉþÊ¤Ê¢Ê¥ÉûÉü³ËêÉäÅíòóÏáÓØ©\‚¯Ùþ„„Ù…ÖÛÑ‰˜ÐÕŠ¿ëô‘™”Î•N•…•ú–™˜|™Tš}š ›ˆœƒœ¤ÆŸ„ õ«{¬]µé¸i¹“¿IÀKÂ}Â•ÆÊo×WÙKÙ‹ãHå•êjê…ê’÷jù|ü›"},
    new string[]{"Shi","ÊÇÊ¹Ê®Ê±ÊÂÊÒÊÐÊ¯Ê¦ÊÔÊ·Ê½Ê¶Ê­Ê¸Ê°ÊºÊ»Ê¼ËÆÐêÊ¾Ê¿ÊÀÊÁ³×ÊÃÊÄÊÅÊÆÊ²Ö³ÖÅÊÈÊÉÊ§ÊÊÊËÊÌÊÍÊÎÊÏÊ¨Ê³ÊÑÊ´ÊÓÊµÊ©ÊªÊ«Ê¬õ¹ÝªÛõîæóÂöåöõêÛéøÝéóßìÂÚÖß±õ§ó§Fd~ËÛƒ½ƒà„Ý…b…„…«…Ú…á†Fßò†‡uˆËµÌ‰PŠ]Š¸‹q‹ÒŒgŒjŒpŒŒËÂŒÆ]«ÖŽŸsåèÊçô^É‘÷Ìá“JË¹•E•g•r–§–É–ò˜N˜V˜t™yÖ­›n››¸Òºœ›œ¢œÒœÛœáÉÌñžøŸ³ªHªLª{¬‹®‡¯a±c±i±x±ìêµu¶_¶ƒ¸b¹E¹GÉ¸¹•ºIºYºº »i½JÀ[ÒïêÈÖ«ÃeÉáÅkÈžÉNÉPÎgÎtÑ|Ñ ÒnÒ|Ò•ÓlÓ”Ô‡ÔŠÕœÕžÖu×RÙBºÕÛJÝYÞyßYßfßmßrß}ßŸºÂáyá‡á‹áŒâPâ‹ââžãAãBãJãväKå~åœæ|îèï¡ïzï†ï—ðOðSâ»âÁñ\ñ‚ô˜õZöXö|öˆö‰øOø[úPû\üœüýaýk"},
    new string[]{"Shou","ÊÖÊÜÊÕÊ×ÊØÊÝÊÚÊÞÊÛÊìÊÙô¼á÷ç·…§‡bˆ–‰Û‰ÞÞÐ’ö”™ÌÎ›ìýª•«F¯l¾RÄfá~æ"},
    new string[]{"Shu","ÊéÊ÷ÊýÊìÊäÊáÊåÊôÊøÊõÊöÊñÊòÊóÊçÊêÊëÊßÊèÊùÊúÊûÊüÊíÊþË¡ÊàÊîÊâÊãÊïÊðÊææ­Þóïøç£ãðë¨ëòÛÓÝÄì¯äøÙ¿‚J‚TÓá‚m‚‚ƒ©ƒÊ†CÊÛËÔÈ¢ŠìŒFŒ¥Œ«ŒÙŽõóXƒ’¼’¿’æÞí”d”µ•¤•ø–XÖì–€èÌ–µ˜Ð˜ä™]šÌšÑä³ˆ©òž‚ŸY¬Ÿ­qñâ¯E°P¶•¸w»P¼^¼‚¼Ÿ½R½ˆÁ›ÇOÝ±ÉDÉ[Ë\Þ´Ë’ËŸÌ ÐOÐWÐgÑVÒeÒlÖ‘ØQÔ¥ÚHÛSÛ\Ü“Ý”Í¸àgÒ°ã_åfçTèCïíêx³ýêœõ_÷n÷tùeùŽú–úžðÖü“âàØ­"},
    new string[]{"Shua","Ë¢Ë£à§ËôäÌÕXÑ¡ßx"},
    new string[]{"Shuai","Ë¤Ë¦ÂÊË§Ë¥ó°…iŽ›½—¿\ËçÀŠ"},
    new string[]{"Shuan","Ë¨Ë©ãÅäÌŒ£–Õ˜¤ÉÇÄYõßéV"},
    new string[]{"Shuang","Ë«ËªË¬ãñæ×‚ö‰u‹þ‘S˜¾™ÜäÈœöwž{ž“µd¿YÆCç`ëpò‚óLóZú{ûUût"},
    new string[]{"Shui","Ë®Ë­Ë¯Ë°ËµŠÜŽœ’¨’Éšì›ä›çµˆ¶ÃŸÑcÕfÕhÕléjãß"},
    new string[]{"Shun","Ë³Ë±Ë²Ë´¿¡çÝÑ²eâþ˜J˜ù±†²i²pÊŠÝí˜ôB"},
    new string[]{"Shuo","ËµÊýË¶Ë¸Ë·Þ÷åùéÃÝôîå†dàÊËÔšFšõ›«ËÝåª d qª“¯Ÿ²´T¹›Ò©ÈpËŽÕfÕhãˆælèp"},
    new string[]{"Shw","ÕÛ"},
    new string[]{"Si","ËÄËÀË¿ËºËÆË½Ë»Ë¼ËÂË¾Ë¹Ê³ËÅ²ÞËÁËÇËÃËÈñêæáÙîòÏØËãáïÈãôóÓßÐð¸æ¦ÛÌçÁìëäùÙ¹ÒÔËÌý‚h‚Æ‚Ðƒ„@…‹Ì¨‡zŠÙ‹wŒKPáãlà–yÎö–Ÿ–Æ—t—ö˜{›q›…›—›åž[Ÿù µ´fìô¶D¶L¶T¸rºôé¼i½z¾ŒÀŸÁQÃBÒÞÝ¾Ê‘ÊœÌŒÎEÎ‡Î’ÏaÏzÒ–ÖpØ|Þ âLâ‘â–ãjãƒäFälæJçrïôï\ïtï~ïï•âÂñ†òIòlúfúƒûýDìá"},
    new string[]{"So","ÏA"},
    new string[]{"Sol","r"},
    new string[]{"Song","ËÍËÉËÊËÎËÌËÐËËËÏñµÝ¿äÁã¤áÔÚ¡áÂâì‚‘‚ö…ºŠ»ØŽôè‘Z‘m‘¡’¿’Ö“K“¡–…–œ–·—s—Œ˜B™€Yë³—Â–ÉÌtÎ@ÔAÕbæJæïÈížðmñžó "},
    new string[]{"Sou","ËÒËÑËÓËÔàÕà²ì¬ÛÅÞ´ïËâÈî¤äÑòô‚Ïƒð…®ŽùC’¿’È“¡“ß”\ë·×å—¯™¸šFä³’ªv¯˜»PÉLÉrË’Öjànágæ}æï`ïbðtòp"},
    new string[]{"Su","ËØËÙËßËÜËÞË×ËÕËàËÚËÖËõËÝËÛãºóùö¢öÕÙíà¼ÚÕÝøä³‚ÑƒD…rà²‡ÕˆTˆUˆ¼‰O‹•å‘ˆ’Û°á“º—V—­˜j˜Â˜É˜þ™Åšƒ›ƒ›«œß’š«T«Ž­X®d´c·B·D·d¸@»¿i¿sÃCÄhÇxË‚ÌKÌVÔVÖqÚxÛ‘ßißpä_ðMò“óXõ‡÷Tú‰ûh"},
    new string[]{"Suan","ËáËãËââ¡…W×«¯iµ{¸Œ¹gºeÑ¡ßx"},
    new string[]{"Sui","ËêËæËéËäËëËìÄòËåËèËçËíËîíõÚÇå¡åäìÝÝ´î¡‚‚‹†a†÷ˆ¼‰åŠÌ‹ÓÀ’µ”ø™pšqšršË›ÔœñÜžvŸ«ŸÕ­j­…²B³Z¶X·[·uºw»‚´â½—¿\¿…¿“ÀZÀŠÃœÄŽÆVÇ]É¯ÈšËòÒ`Õr×\ÙwßUÒÅßzçiçwç›êyê ëSëmì[ìší}ól"},
    new string[]{"Sun","ËïËðËñé¾Ý¥â¸áøöÀ†ÐŒOŽ…’X“p“q“˜Ê÷˜ƒ–ªs®p¹S¹º‹ÉpÊ˜ËVõÐæ{ïŠ²ÍúZ"},
    new string[]{"Suo","ËùËõËøËöË÷ËóËòÉ¯ËôêýíüàÊßïèøàÂæ¶ôÈÐ©‚é†î‹‘ËêÀ»³­’­“™•­šqÉ³œÅœàÎþ Þ«I¬R¬­Fºwºz¿W¿sÇjÈšÎRË¥Ñ–ÚtåÒßCæ\æaæiææ•»ôì[óšô‹õ€"},
    new string[]{"Ta","ËûËýËüÌ¤ËþËúÍØÌ¡Ì¢Ì£äâõÁ÷£í³é½äðåÝîèãËàª‚@‚è…ì…ú‡–‡Å‰‡Ì«„“‚´î“Ò“é˜dšÏ›øÊªœÍßêñ ­ªH«Hµk¶NÇEÑÕw×nÜDÜc´ïÞ…Þ‡ßQß_ßeãBåJæ]ædêFêSêYê`ì‘÷²ìŸíOí^õ]ö"},
    new string[]{"Tae","ˆ‚Òk"},
    new string[]{"Tai","Ì«Ì§Ì¨Ì¬Ì¥Ì¦Ì©ÌªÌ­ìÆëÄõÌß¾öØîÑÞ·Û¢ææƒˆƒè…õ‡òˆr´ó‰ûŠU‹êŒLçö‘B”E”Á•@–Ÿ™…œÌkžå M«}¹x»F»†ÄÜÅ_ÅvÇ ÔrÚ±Ü–áââ‘ïUñ~õT"},
    new string[]{"Tan","Ì¸Ì¾Ì½Ì²µ¯Ì¼Ì¯Ì¶Ì°Ì³ÌµÌºÌ¹Ì¿Ì±Ì·Ì®Ì´Ì»îãÛ°ïâïÄñûå£ê¼ìþµ«‚„‚èƒNƒ{†®†ú‡@‡c‡dˆÅ‰›‰ ‰¯‰ÂŠòŽ—Z´‘…‘˜‘Ÿµ§“Ú“Û”Z”‚•Æ•Ò˜W™AšUÉòµ­Õ¿œžh ž©Ñ×­f¯a°D°c¶U¾gÀWÀ—Àµ¨ÅjÌòÅlÝ¡ÈIÊnÞ¦Ë“ÑgÒfÕ„×T×ZØØÙyêæá]áaávãgåUîtðZú‚ül"},
    new string[]{"Tang","ÌÉÌËÌÃÌÇÌÀÌÁÌÌÌÈÌÊÌÆÌÂÌÄÌÅó«éÌôÊõ±è©ïÛÙÎâ¼äçñíàûï¦ó¥¹‚«‚Úƒ¯„¨†°‡RˆnÉµ¯Õ‘ÜÀ©“­”U”†•ò˜y˜ü™éœ«fgŸ¶ C ‡²˜´g¶KºLº‚¼C¼QÄgµ´ÉyÊŽËTÌoÎvÚZÛ}ÛßTàoæhæ†çMç|èKè’îõéEé‹êOêWãÑëGíUðhðnðyúSühü‘"},
    new string[]{"Tao","Ì×ÌÍÌÓÌÒÌÖÌÔÌÎÌÏÌÕÌÐÌÑØ»ä¬ìâßû÷Òèºß¶—„ü†G‰ú‰üÒ¦‹—Žµ|þ’qÌô“†—ƒ˜…™„›ìý c¬•µŽ³ï»I½d¾I¾T¿_¿l³ñÀ‡ÎIÑiÓ‘Ô|ÖzÌøÝÞá[ä•åcìŠì’íNíwî\ï‘ðuñŠòP"},
    new string[]{"Teng","ÌÛÌÚÌÙÌÜëøƒ\ƒ£Ž¸b¯\»L»T¿gÄ†Ì„ÎŸÖ`ß‚ìLñòvóIöŒü’"},
    new string[]{"Ti","ÌáÌæÌåÌâÌßÌãÌêÌÞÌÝÌàÌäÌéÌçÌèÌëõ®ðÃç°ç¾ÙÃñÓåÑÜèã©‚m‚¨…††—†Ù‡¢ŠDŠ¸‹X‹qŒÏ¨ÃwµÜÊÓŠµÉ‘øÕÛ’«’ó“W”`ÊÇ–õé¦˜NšYš›¢œvzµÒ«Ÿ¬v­ƒíû´Y´f¶_¶”·a»G½¾ŸÁHËÁÆlÊƒËSÌŒÎyÏsÐ}Ñ{Ñ|ÔgÖBÖpÚ®ÚÐÚ„ÚŒÔ¾ÛyÛ‡ÜSÜnÜƒ´ïÞ…Þ‡ßPßXßmäRåaåç‘Îýî}òfóeówóƒóó›óžõkõ{ö[÷–ø˜ùYù•ù—úeúfú‚ÞÐ"},
    new string[]{"Tian","ÌìÌïÌíÌîÌðÌòÌñÌóµèÞÝîäãÙãÃéåî±‚ƒÌµäÍÌ…×†ŠàÁ‰\ŠÇŠõŒ…ŒÄ¤’×“•‹èé›pÕ´œLœµá¬_¬™­k®\µéî®®s®x®ƒ¯t±]±™²V²_´[´k¸K½G¾gÃbÅjÅqÉ»ÈJ²ÏòÅÓCÓ`ÙqáLâšã”å`åŸææ‚èï»ÕòêDìjìpìtîŒîµßï›ø‰úcúlüV"},
    new string[]{"Tiao","ÌõÌøÌôµ÷ÌöÌ÷ö¶óÔìöòè÷ØÙ¬ñ»öæÜæôÐƒ©†GßúÒ¦‹àŒiŒýGŽçf”Ó”þ•q–I–]ÌÒ—l˜Ôµx³í¸I¼g½rÂwÃxÃ‘Å—ÆKÈVÉ‚É‰ÉŠÏCÒ›ÕAÕ{³¬ÚqÚ}õÖã“äpæxï¢ì›î\õöœýf"},
    new string[]{"Tie","ÌúÌùÌûÝÆ÷ÑƒcÕ¼…ãGÂÂzÍuµûÙNÛ@âŸã@ãŽä~ç“èFï°ï”òø‡"},
    new string[]{"Ting","ÌýÍ£Í¦ÌüÍ¤Í§Í¥Í¢ÌþÍ¡î®ÜðîúÝãæÃòÑèèöª‚D‚K…ˆˆNµìŠcŠÇµŽØŽßadâ—H—þ˜w›àœsìŸNŸP«ž¬E¹j½–Â[ÂŠÂ—ÂŸÂ Ã‰ÆJÎbÕPÖFß‹äbéƒì˜îcïFüž"},
    new string[]{"Tol","h"},
    new string[]{"Ton","ª–"},
    new string[]{"Tong","Í¬Í¨Í´Í­Í°Í²Í±Í³Í¯Í®Í©Í«¶²¶±ÍªäüÜíÙÚíÅá¼âúÙ¡àÌÛí‚£Ù×„¨„ç†L‡ìMdŽäÓÁ‘Q‘q•z•Ó–S˜¿™HšÔ¶´›Ïœ§žçžúŸ×Ÿü ‚ ÕªIª‘¯]±íÏ³‹¶‚·r¹c»½p½y½ŠÄ€ô¾ÉŒÍUÎVÏxÐhÔ˜ÚUÖØãPãnã~ï õj÷‹Øç"},
    new string[]{"Tou","Í·ÍµÍ¸Í¶î×÷»Ùï‚ÊˆÇŠ‡‹U‹Óä”«”Óš†¼}½‘¾–ÐåÌeÑˆÖIÖOÚÍÚÏ¶ºäWæBî^ïŒüW"},
    new string[]{"Tu","ÍÁÍ¼ÍÃÍ¿ÍÂÍºÍ»Í½Í¹Í¾ÍÀõ©Ý±îÊÝËÜ¢Óàƒ·ƒò†l†ž‡íˆDˆEˆMˆà‰T‰©Œ_xÄáŽêOL„’¼’Ø“\“Ÿ”¾¶Å—^™y›B›Þœ£¤¬Ÿ¯f¯…¶d¶•¹\ÄRÄ]ÇÈ‹É\ÚgµøÛTÞƒâQâŠäWäŒå„ñGòBùIùWùrùúhú“ýC"},
    new string[]{"Tuan","ÍÅÍÄî¶ÞÒåè„Œ„–‡âˆCˆF‰t‰’‹§Œ£‘_“»¶Ø˜¤™ˆœ¨`Ÿ™ªl®™´u¶Ë°ºi¼aÉ”ÑƒÑ‰Ø‡æ˜÷Hù‡úoú™ðÈ"},
    new string[]{"Tui","ÍÈÍÆÍËÍÊÍÇÍÉìÕß¯‚M‚QƒU†”‰‘ŠÑŒ¾wµÜL˜ú¶Ë°·~ÂvÃ“Ã•ÍÑÉ—Ë”ÌLÍ‘Öd×‚ÛƒÛ×·ëPîjîkînðÀ¡òDòoóhôs"},
    new string[]{"Tun","ÍÌÍÍÍÊÍÎ¶ÚÙÛâ½ëàêÕ¶ÖÎâ…×†”‡pˆdŽÝ÷‘‡•H–N›Iãç›âìÀŸlŸõ®™ñ¸¼ƒ´¿ëÆÄ†Ä™ÆXÎPØZÜ”Öðå`ë˜ï‚ô÷ƒü`"},
    new string[]{"Tuo","ÍÏÍÑÍÐÍ×ÍÔÍØÍÕÍÖÍÙÍÒÍÓÆÇéÒèØõÉØ±ÛçÙ¢âÕõ¢èÞö¾ãûóêíÈËûšë‚M…ï†®‡cˆ÷‹s‹µËüA±¶è’L’„’¨’É“ã–l–s—‡—ø˜’™Eš¼šÍšú³Ø›k›ñ ­ ö³a´P´u¶Ë°»X½FÃ“ÅbôªÇhÈ[ÌEÉßÏ€Ð†Ð‘Ð›Ó”ÓšÔqÕfÕh×™ËµÚ—Û|Ü€Þ~åÆÞãBälîèêeêuËåï€ð˜ñWñXñjñ„ñ…ònò™óCô…õDöz÷WørùKüƒü˜¶æ"},
    new string[]{"Uu","ŒËÁ”É•é–G–þš`šµš¶šÄ› ŸHŸeŸ‡ H°i°›µs·EÂSÂqÂ‰Å†ÆŠÉIÉ…Ë€ÍCÎ_ÑCÚJÜxÞmåwæç”ê[ìTù"},
    new string[]{"Wa","ÍÚÍßÍÜÍÛÍÞÍÝ°¼ÍàØôæ´ëð„¾…÷†„†œ†å†ìŠ¹‹zŒÜŽ’º“‰”…›@›AÎÛœÎj­ ®|³[·Š·“·˜¸DÂvÄeÒmÖœßœì…Ð¬ícíií€õqöÙüpü|"},
    new string[]{"Wai","ÍâÍááËßÃ†J†·Ø²žx¸î“"},
    new string[]{"Wan","ÍêÍòÍíÍëÍæÍäÍìÍåÍèÍóÍðÍñÍéÍçÍãÍïÃäÍîÂûÝ¸ëäòêçºÜ¹çþæýØàîµÝÒ^‚{Ãâ¹Ø„\…d…e†nÔ°ˆ¾‰G‰Ï‰í‰îŠ€ŠþŒRŒñŽ¦ñ­’e’Â’Ì’ç•Š•–ëÃ–v—i—µ˜´š÷›ðŸÏž³¬T±D±›¸Šóî¼w½ƒ½Œ¾O¾UÂDÃÜÈÇ{Ç|ÈXÈfËHÍWØ™ØžÙ–Ú@¹áÛlÝkÝnßà„ä[äjä‘åså†æ~évêKêPîBó[ó\ó]ô’"},
    new string[]{"Wang","ÍûÍüÍõÍùÍøÍöÍ÷ÍúÍôÍýÃ¢éþ÷Íã¯Øè“©ƒÇ¿ïÞÌŒ²ŒµŒ¶Œ·´¸ºû’[•™–M–R—Ÿž_Ÿƒ¬]»Ê±Z¾WÇwÈDÍ^Í‡Î\ÕsÝyÞ‚ÞŽ"},
    new string[]{"Wei","ÎªÎ»Î´Î§Î¹Î¸Î¢Î¶Î²Î±ÍþÎ°ÎÀÎ£Î¥Î¯ÎºÎ¨Î¬Î·Î©Î¤Î¡ÎµÎ½Î¾Î«Î³Î¿Î¦Î®Î­Î¼ÒÅÝÚàøôºöÛæ¸åÔãÇÚñãíçâä¶á¡áËÚóÚÃä¢ÙËâ«â¬áÍê¦è¸ì¿ìÐÛ×Þ±ðôÙÁ‚¥‚Îƒ^ƒ¤…y…°ßà††Â†Ò‡ˆàí‡úµÌ‰Š‰Ã‹W‹n‹yŒ¿ù_e™½ŽUŽhŽ®@ÎiÌÎ‘£’Ë’Ú“G“f“Ö“ã”Í•¥ÓÐ»ú—|—Û—Ü˜L™Þ›W›”›¾œwœ‘œ¿œÕ‘¬èžHžSžwžéžùŸŠŸ˜Ÿ£ìÙŸÝ V ‘ Ò àâ¢ªc¬^¬|­M­Ž¯_°I°Líõ²z³u³}´S´j´oÁ¢¾S¾“¾•¿JÁWÄ^ÆYÜÏÆ„Æ‘ÇUÝ´Ç‹ÝÌÈ”È–ÉJÉ–ÊlËeË—ÌvÎOÎTÎVÎkÎoÏGÐlÐoÒEÓAÓWÓ}Ó‚Ó„Ô•Õ†Ö^×~×ˆÚ~ÛbÛcÜZÜ^ÞEß`ßzàŒáWåMå…å—çAéÚãêžö¿ìGìSífílítí|îQï]ð]ðjðŠó[ó\ó]õKõdõnögöhöz÷˜÷"},
    new string[]{"Wen","ÎÊÎÄÎÅÎÈÎÂÎÇÎÃÎÆÎÁÎÉãëãÓØØö©è·Ãâ…Ð…Ø…Ý†–‰eŠpÃä¨ëìã³‘C’^“h“‹ÃÁ•j—S˜X˜všzéâšœbœØžÉŸ±«œ¬¯‡ÑÛ³R·g·€óË¼y½ƒ¿A¿ZÀˆÂ„ÃWÃÃ‚Æ[Ç|Ê•ÔÌÊŸËœÌNÍPÎÏRÑŽØnÝ˜ÝœÞdâ†æ’éé”éšêZÏÞíyè¹î‚ðwñbô•ö€ö“÷—øYøjøsü•Ùï"},
    new string[]{"Weng","ÎÌÎËÎÍÞ³Ýî„Ø‰RÛÕŠTÇ•²œå®Y²\ÀšÂÃÉÎŠæfúOûlýN"},
    new string[]{"Wie","ÄŽ"},
    new string[]{"Wo","ÎÒÎÕÎÑÎÔÎÎÎÖÎÏÎÐÎÓÙÁá¢ö»ë¿Ý«à¸ä×íÒ¥‚¬†àÉ‡f‡—ÛöˆåØ²ŠðŠñ‹_æÁ‹‹’Ó’Ú’Ü“ë”Nè»–†—ç›ð›óœuãüŸsªi­xÎÁ²Y²ˆ¸CÄOÄŸÅPÅŸÈnÉ^ËhÎÛbÛlëoñNý}ýŠ"},
    new string[]{"Wu","ÎÞÎåÎÝÎïÎèÎíÎóÎæÎÛÎòÎðÎÙÎäÎìÎñÎØÎéÎâÎçÎáÎêÎÚÎã¶ñÎÜÎßÎ×ÎîÎàÎëåüòÚêõå»Ø£âäÚãÚùâèæðì¶öÈØõè»ðÍæÄåÃðíÜÌìÉâÐðÄ÷ùä´ÛØWÍö“Øî°Ù¨‚W‚—ƒƒÇ„„Õ…Ç…Ò†•à¸†è‡fÛëˆºˆé‰]‰ŠVŠÃŠÓŠÕ‹³ŒäŒí}¶ÎŽÄTvÓùí’šº‘“’G’H’N“h““”–ì¸•J–f–g–Ã˜îšTÄ¸›@›A›^›ž›´œrä×œ×ŽžõŸoŸ½ŸÊ«b« ¬@¬­N®Wî¦²y³J´IµµŸ·—¸PóË¹™»|ÄŠÆ•Ç`ÄªÊÌFÎÏwÕGÕ_Õ`×OÚÜRßAàNàwâEänäoåqæuèžê‚ëFë‰ëœìFìWì}ò\õˆöƒøŒùMù^úFú~ûcýHýIýrö¹ßíòú"},
    new string[]{"Xi","Î÷Ï´Ï¸ÎüÏ·ÏµÏ²Ï¯Ï¡ÏªÏ¨ÎýÏ¥Ï¢Ï®Ï§Ï°ÎûÏ¦Ï¤ÎùÎõÏ£Ï­ÎþÎúÎôÏ±ÎøÏ³Ï©À°ÎöÏ¶ÆÜÏ«Ï¬òáÞÉä»Ýßâ¾åïçôæÒìùÙâôâñ¶ìûÙÒäÀÝûô¸õèõµì¤ðªó¬ôËÜçáãÚôßñêØó£ì¨éØãÒôÑìäêêÝ¾÷ûñÓôªÒå‚S‚`‚Ý„D…[…c…k…s…w…À…ä…è†Aß×ßÒ†Œ‡q‡›‡½‰I‰¸ŠGŠÖ‹f‹ÄŒjŒÁÊºŒÊŒÚÆíŽQŽ`ŽdK¹ÐæèïOY_¦÷ø‘‚‘ƒ‘ï‘ñ‘ò’Q’V“©Ëº“ô¼È•„•‘•Ê–y– —N—«—Ì—á˜~˜›˜é™S™úš@š]šâšãÈ÷œlÊªœëSd”•Àñž¢ŸXŸ_ŸmŸyŸ›Ÿ¼ŸÁŸçŸèŸù O x Ì × ÞªLÁÔª“«I¬N­t­Œ¯Œ±_±–²q²—´F´Ž´—·G¸O¼Y¼š½”¾k¿]¿uÀGÀMÀ{À…ÁpÁxÁ•Á—ÃZÃ[Ð²Ã{Ã|Ã~ÅbÇbÇmÈ}ÉYÉjÉtÊDÊ“Ë@ÌŸÎEòæÎ€Î‰ÏkÐPÐañÞÒuÒ‚Ò ÓBÓ}Ó‚Ó„ÔDÔqÕOÖLÖlÖuÖ×@ÚÀÚÖØGØHØgØlØ‰ÚTÚVÚiÚvÛ’ÜhßgÛ§àEàSàqá@áâMâRâlâ|ãbãcãŠåaåeæˆç^è„è•´íêSëKë^ëvëìIìUïeðFðOðqðœòwò„óNô]õ–öw÷@÷^Èú÷žùTú ü_üŸýAÛ­"},
    new string[]{"Xia","ÏÂÏÅÏÄÏ¿ÏºÏ¹Ï¼ÏÁÏ»ÏÀÏ½ÏÃÏ¾»£áòèÔßÈ÷ïíÌóÁåÚè¦B‚b¼Ù‚Ò…­Ñ½ßêàÄà¾‡˜ˆYˆ®‰ìŠAá¬{BË‘³Ñº’Ü“Š”¯•g—šBì¦ë¥ä¤›ÑœÀžÙžþŸªM«”¯K¯Pðý²L³ˆ´W´lµ„¸—¹d½o¿E¿[¸øÁŽÅrÅ{ÆSÝçÊ›ÎrÎ˜Õ’ÖlØBÚYÝ àAå’æ_ç]éiépêƒê˜ïPòhô öyúT"},
    new string[]{"Xian","ÏÈÏßÏØÏÖÏÔÏÆÏÐÏ×ÏÓÏÝÏÕÏÊÏÒÏÎÏÚÏÞÏÌÏÇÏÉÏÙÏÍÏËÏÜÏÏÏÑÏÛÏ³¼ûÜÈÞºá­ðïÝ²ôÌæµò¹áýììÙþìÞõÐõÑõ£åßë¯ðÂóÚö±™½ÁÙ‚]ƒMƒgƒmƒn…î…û†Z†m†¥†é‡JˆŸˆÉ‰A‰·Šhæ¡ŠˆŠ™æ©Š«Š·Š½ŠÒŠÞ‹M‹¸‹¹‹Í‹üå¾Œ¯Œ°ŒÝsŽMŽÒ`üã»‘a‘‘—‘œ‘¾’¦º´Ì½“`“y“{“È“Í”g”s•–}–ž—g˜˜ó™Ì™÷šÀÈ÷Ï´›×œ¶œÇ½å¥žnž¢ž¶žóªAªªž«I«N«t«ˆ¬F®Q°B°G±]Ê¡±h±•²vÒÓ³w¼î´šµUµ ¶[¶i·SóÈ¹a¹‘»˜¼`½L½m½½ž¾Q¾€¿h¿„¿ÀoÀwÀ‰ÁwÁ{¼çÐ²ÃjÃ{Ã|Ã~ÄdÅ@ÅOÅ`ÆxÇ{ÝüËWËÌ\Ì_Ì`ÍpÍ€Í˜ÐjÒDÒvÒŠÕ^ÕtÖPÖ›×]ØRÙtÚDÚ`ÛŸÜ]ÜŒÜŽÝÐùá_ázáŸãŠã”ã•ä}åDåUåvå‚çoèvîÌèïÄéeéfêRãÛêˆê“ëUí`í„í†îyî‡ï@ðWðuñMõröx÷€úNú‘ú’úšûyû’üGíéýE"},
    new string[]{"Xiang","ÏëÏòÏóÏîÏìÏãÏçÏàÏñÏäÏïÏíÏâÏá½µÏèÏéÏðÏêÏæÏå÷Ïößæøó­âÔÜ¼âÃç½ÝÙºà‰ß‚íƒ¨„â„ð†“†”‡»Š¢ñŽûÝÈÁ•}•Ú–Ù˜U™ÖÑó‹«“­­˜½|¾|ÀvÀ‘ÁfÄô­ÈeËGÌZÍJÏ†ÐiÒVÔ”ÛKÞ†à_àlàmàxã}ãätç}è‚é{é•é—í‘í—ðAð‹ð“óJô\õaõœ÷P÷`÷zø—û‘"},
    new string[]{"Xiao","Ð¡Ð¦ÏûÏ÷ÏúÏôÐ§ÏüÏþÐ¤Ð¢ÏõÏýÐ¥ÏöÏøÏùÐ£÷ÌòÙæçèÕßØáÅóãäìåÐèÉç¯óïÙ®‚PÇÎ‚j‚å„¿…®ÛÅºÅ…ëºô†DßÝÉÚßë»£†’†Û‡C‡E‡V‡Z‡[‡^‡Æ‡Ìæ¯ŠëŒnŽé–k|‘‹ÄÓÉÓ“`ËÑ“Ï“ß”¬”Â”Ã•š•Ô—nÉÒ˜þ™ÏšRšYš^š¥š®›©›ßœøx’Ížtž¼ž½žñŸ^ŸÀŸêŸò _Ø³½ÆªVª’ª”¯e¯h°~°†±³‡·n·›¹G¹q¹›ºSº}º½g½‹¿„½ÊÁ›½ºÃ‘Ä…ÉÖÜúÇzÈpÊ’Ë@ËrÌ‡ÌÏSÏ]ÏvÐDÔFÔ‰Õ[ÕqÖjÖyÖ—×DÛXÝ^ÞB½ÏàUäNïYò}ò”òœ½¾É§ófónóuø{ø“øŸújúrû^"},
    new string[]{"Xie","Ð´Ð©Ð¬ÐªÐ±ÑªÐ»Ð¶Ð®Ð¼Ð·ÐºÐ¸Ð¹Ð¨Ð°Ð­ÐµÐ³Ð«Ð¯Ð²½âÆõÒ¶ç¥ò¡çÓâ³é¿âÝß¢ÙÉå¬äÍÙôéÇåâÞ¯õóÛÆÛÄÄ‚´‚ÄƒDƒªƒæ„µ…f…l…Ãßñà®†à‡ƒˆ•‰f‰êŠAŠGŠÀ‹rŒ@Œ‘ŒÈŒÏŒÑŒÔŒÚlŽOÇeïø’’¶½Ó“a“yß¡“û”X”y”ý•»–¤½Û˜f˜®šGì¨š¢Ö­›ªœœ¸Èœë¢ÊžažÂžàžáŸLŸcŸ» X yªn¬€íõ´cµm¶c¼I¼œ½X½e½u½’¾Š¾™ÀTÀiÀ‹Á–Ò®Ã{Ã|Ã~ÄnËZË†ÎdÎqÏÏ’Ð~ÑWÑ€ÒCÒpÓiÓnÕ™ÖCÖx×f×µýÔ¥õÍÜaåÈæEèHÚôíCíPí…í“îRÒ³÷ºôkõ@õqöÙýKý^ýaýkýšÀ£"},
    new string[]{"Xin","ÐÂÐÄÐÀÐÅÐ¾Ð½Ð¿ÐÁÑ°ÐÆÐÃì§Ø¶Ý·ïâÜ°öÎê¿²¿‚rÐË‡Œ‡Š|Š®‹×ŒJŒ¤ŽßQ¹×‘€“Ú–‚–“™AšE¿îšL€žÔŸ{±^µUÃ’ÅdÅgÒWÔDÔMÜŒß”á…âdä\ç†êcîˆñQñ^ôg"},
    new string[]{"Xing","ÐÔÐÐÐÍÐÎÐÇÐÑÐÕÐÈÐÌÐÓÐËÐÒÐÏÐÉÐÊÊ¡íÊã¬ÜþÚêß©Üôé‚††Qˆgˆlˆž‰DŠÈŠü‹”‹ñŽy•Û›™›ëœîŸ“ŸÉ õ¬w°‹²MÑÐ³x¹“¹ž¾mÊ¤ÅBÅdÇnÍÑRÓqÓwÖ_àDâ]ãoã‹ätè—è™ê€ðhâ¼òHóUõSö]"},
    new string[]{"Xiong","ÐØÐÛÐ×ÐÖÐÜÐÚÐÙÜºƒ´†M‰éÏÜúr”¸•d›°Ÿ‚ÃrÔKÔwÔž×›×œÙ‚ÚU"},
    new string[]{"Xiu","ÐÞÐâÐåÐÝÐßËÞÐáÐäÐãÐà³ôäåõ÷âÊ÷Ûð¼ßÝâÓá¶‚c‡›˜¼™Ïœúžñžòìã«‹¬L­P¼N½½‘¿ÀCÃƒÃ‘ÅWÅ^ÆvÆ’Ý¬ÉŠÎÑfÑ„Ñ…ã–äPæTæ™çVçnïqð}ó…õx÷Gø ýM"},
    new string[]{"Xu","ÐíÐëÐèÐéÐêÐîÐøÐòÐðÐóÐõÐöÐçÐìÐñÐ÷ÓõÐïÐôÐæôÚÛÃèòä°Þ£çïÛ×äªñãõ¯Ú¼äÓìãíìí¹ÓÚ€­ÐÝÅÓàò‚T‚»ƒÛ„Ô…rÅ»…éºôßÝ†ÄÐá‡I‡b‡uˆ¦‰ÙŠˆŠ½ã‹€‹ÁŽ­Vâðj±‘A’î“T”›”¢ê¸•B•d•v• •ýëÔ—ì˜™øšAšHš[š_š~›T›UœMœ•œäGs…žíŸTª««—¯L±N±S±r²W²x²œ·P·V· »n¾A¾w¾{¾–¿HÀ]ÀmÂ…ëÉÃaÓóÆRÆ^É[É’ÊŒËvË…ÌÌ“ÍmÍ‚ÎdÒŽ¹æÓ’ÔSÔ[Ô‚ÕšÖ[Ö~ÖŽÖžÚ©× Ù[Ð°à†ã_ã„è`ö§íšíœÙåòôPôqôzôˆ÷r"},
    new string[]{"Xuan","Ñ¡ÐüÐýÐþÐûÐúÐùÑ¤Ñ£Ñ¢È¯êÑé¸ÙØäÖäöãùîçè¯ìÓíÛïàìÅÞïÝæðçÚÎØ¨…º†I†¿‰H‰éŠˆŠ®‹l‹Ÿ‹Ö…RËÐ‘¤‘Ò“E×«ß§•R•]•t•œÅ¯•Ã—]˜C™eä­ä¸×Ÿ@ŸœªBª™«R«t¬I¬K¬u­v­‚°_±P±†²U²¶P¹Ž½L½k½¿h¿¿’¾îÂAÂQÈkÉ{ÊRÊžÌBÌTÍ•Í›ÎhÏÐfÐžÕÖX×X×zÚKÜŽÞFßxß€ãCämæMæ›èGéIìœïXð‚âÍñòCö~"},
    new string[]{"Xue","Ñ§Ñ©ÑªÑ¥Ñ¨Ï÷Ñ¦õ½àå÷¨í´l¾ö„ä…ÉÏøàëˆy‰®ŒWŒúNŽGV”Ä–ù—]˜Ý›Q›‡›‰œéÍžyÈ²žû K ü¯N¯T²xÄ}Å–Æ‹ÉHÐÓ{ÖoÚÊÚpÞGÞjëzíYí|÷Lú›û`"},
    new string[]{"Xun","Ñ°Ñ¶Ñ¬ÑµÑ­Ñ³Ñ®Ñ²Ñ¸Ñ±Ñ´Ñ·Ñ«»çÑ¯¿£Ùãöàä±Û÷âþâ´õ¸ä­Û¨á¾Þ¦Þ¹Ü÷ñ¿êÖáß¾ù‚Å„×„ë„ì…_†C‡e‡x‡ ˆ_‰_‰¶‰ËŠQŠ®ËïŒOŒ¤eãªô“M“Í”–h–Õ—DËó˜ßš¦š½È÷Ì¶¡žFžµŸ[ŸŸïŸñ @ ` o wªFâ¡«‘­R±†²†¶¹S¹oºJ¼r½kÀcÑ¤ÄÝ¡ÈÊMÊnË`ÌQÏrÏyÒWÓÓ–ÓœÔƒÙbÞ™åÒ¶Ýßdà‰èRöÎîšñZñ÷S÷\úZåæ"},
    new string[]{"Ya","Ñ½Ñ¹ÑÀÑºÑ¿Ñ¼ÔþÑÆÑÇÑÄÑ¾ÑÅÑÃÑ»ÑÈÑÁÛëñâí¼çðèâíýæ«ðéá¬ë²ØóåÂÞë^„†‚o‚œ„²…|Ñá…ƒ…’ß¹Îá…ì†s†¡ˆBˆLˆRˆºˆ×ˆÛ‰ºŠ´‹IŒSŽŽÞŽâÓù’~’¥’éÔý–‘—¿—âÐªšå›ÅQ”žõ ëªcªm«e¬ˆ¯P¯{íÙ´l¶–·Š¸E¸ŽÂyÊ‹ÒÓ ÜˆÝ`Ý‘éûÐ°âXåEçŒè›élÕ¢î†ÑÕøfø†ùgùsù“ðÆýGý\ý…ÑÂ"},
    new string[]{"Yan","ÑÛÑÌÑØÑÎÑÔÑÝÑÏÑÊÑÍÑ×ÑÚÑáÑçÑÒÑÐÑÓÑßÑéÑÞÒóÑËÑâÑãÑäÑåÑæÑÑÑÜÑèÑàÑÕÑÖÇ¦ÑÉÑÙÜ¾ØÉãÕÝÎ÷ÊçüäÙìÍØÍóÛëçÙðØß÷Ðâûî»éÜäÎÙÈÚÝëÙêÌãÆÙ²Û±õ¦Û³åû÷úáÃÚçæÌm°³‚©‚¹ƒBƒ°ƒ¼ƒÒ„‰…]…y…’…—…˜†m†Ç†Í‡{‡²‡À‡ÙÛïÛû‰c‰†‰Á‰ÌÏÄ‰üŠzŠ°Š¶ŠÔŠ×‹j‹Ç‹Í‹é‹÷ŒEŒß°»¼öŽMŽiŽrŽsŽtŽvÑ²¹ãâÖem©Ý‘±‘î‘þ’Z’¨’´’É’ï“C“RÞî”©•V•••¶•à•ó—¦—â—ã—ð˜Ü™L™•™¿™ëš‡ë³›Wä¦›¡ÏÑ›þµ­Òùœ{œœ¶œÄvž ž¥ž·ž¹žÏŸSŸgŸŸŸÌŸð w ²ºÝªPª_«Š¬J­’®[³x³Ž³š´Nµhºcº™½ž¿tÏÛÁwÄdÅEÆFÆGÇrÝ²ÈCÈTÈ€ÈŠÊBÄèËWÌšÎiÑsÑŠñûÒÓ_ÓƒÓ…ÔPÔÖV×…×—Ú¥ØVØWØ]ØbÙžÚIõÂÜyÜ‚Õâß@ßVàIàáDáZázá€á‰ãUåUïÄéZéŽéé‘êmÏÕêŽêšëCëUØÌìvîîƒî†î›ðòVòYòzòžóFôeô|öoøHøNøeø‘ùžú`úŽû}û’ûšüGüdüfüiüjükümüsýBý]ýdýzý‡ýŒ"},
    new string[]{"Yang","ÑùÑøÑòÑóÑöÑïÑíÑõÑ÷ÑîÑúÑôÑêÑëÑìÑðÑñì¾í¦áà÷±ãóòÕìÈâóÖ‚ê„½„Ø…n…óˆtˆ”ŠIŠš‹PŒ¢Œ÷¤§û‘Ä’t“P”a”®•D°º•[Ó³•ª–³—î˜D˜”˜ÓšTšÞšçœ«‹žYžæŸ¬«Œ¬„¯ƒ°W±j±ˆµS¶@½DÁfÁkÁnÁyÃoÓ¢ÔhÔ”ÖUÏêÝIÝŒãZå}åç{è–êgê–ë‡ë›ìRï^ïrï…ðBñöuø„ø—ûF"},
    new string[]{"Yao","ÒªÒ¡Ò©Ò§ÑüÒ¤Ò¨ÑûÑýÒ¥Ò£Ò¦ÑþÒ«Ò¢Ô¿½ÄÌÕÔ¼Å±çòØ²÷¥ðÎé÷Ø³ßºï¢çÛáÊëÈê×áæèÃñºÃ´ÀÖ¦‚x‚¶‚çƒe„üÄö†º†Ú‡y‡§ˆˆòæ¬‹Q‹„‹ÆŒaŒ¸ŒëiŽAŽCÓ×ÙQfç’q“e“u“Á“ê•¬•ê–”–Ì—ê˜e˜l˜·š|š¥ä¬ÒùœÈœôå®žìž÷ŸÆ d úªqªrª’«Q¬ŽÓÉ±l²‡´tµn·Ž··š¸G¸H¹O¹–¼sôíÀfÅ—ÆwÜéÈ™É@É|ÊËaËŽÌiÐ‰ÒÔ@ÔoÖ{Ö|×ŠÚŒÛuÝUßbã“æcè€é™êœì‰î–ï_ïuïŸðPò[òˆæñöŽø^ø€ú_úrýGýo"},
    new string[]{"Ye","Ò²Ò¹ÒµÒ°Ò¶Ò¯Ò³ÒºÒ´Ò¸Ò±Ò­Ò®ÑÊÒ·Ò¬Ð°ÚËÚþêÊìÇÞÞîôØÌˆ‹‚œ‚´ƒp…½†œºÈ‡S‡™ˆ¸ˆìÊû‰¢‰­ÉälŽIŽJ‘±’w×§’À’ÅÞîÞé“ü”@”I”K”LÐ±•¢•Ï•Ð•â–‘–¥–¦˜G˜IšSš†š‡›ä¦Í¿c£ÖŸ¤ŸºŸî { ”­’¯u°‡²w²|µB¸E¾ŠÄôÂ™Ý±È~ÍFÐJÐMÖ]àvà’âPâXäyæEæUædèHìví“ðNðYðvð†âÅñ@óBùwûEüg"},
    new string[]{"Yi","Ò»ÒÔÒÑÒÚÒÂÒÆÒÀÒ×Ò½ÒÒÒÇÒàÒÎÒæÒÐÒÌÒíÒëÒÁÉßÒÅÊ³°¬ÒÈÒÉÒÊÒËÒìÒÍÒ¼ÒÏÒêÒ¾Ò¿ÒÓÒîÒÕÒÖÒïÒØ¶êÒÙÎ²ÒÛÒÜÒÝÒÞÒßÒÃÒáÒâÒãÒäÒåÒÄÒçÒèÒéâøðêï×ñ¯âùæäì½ìÚôýñ´ì¥á»ÛÝéìàÉß×Ü²àæØæÚ±âÂäôØýßÞðùâ¢íôôàÞÄÜÓÜèØîÙ«êÝîÆçËåÆØ×ã¨÷ðôèß®ÞÈòæÛüÞÚáÚÞ²ß½éóïîô¯Ø¯UVÒ²{±ÊÌÙ¢î‚X‚Ãƒxƒ|ƒŒƒÏƒÞ„·„Ö„ã„ù…FÓ¡…¥…¬Ì¨Ì¾…ÀÌý…å…ê†jÒ­‡ÒˆIÛÙˆ`Ûçˆ£ˆË‰©‰Ò‰ßÏ¦Ê§ÆæŠ‰¼§‹f‹‹¡‹Â‹Î‹ÚŒTËüŒbŒhŒ•Œ–Éä[Æé©ŽFŽKËÈŽƒŽ•Ž–Ž¯ŽåMo‚£¤¥±æ@µ¡q–‘›‘«‘÷‘ü‘ý’LÔñ’Þá’íÞõ“~“Ì“ñ””§”¹”¾Ê©”î•i•”••Ë•Ù•ö–p–s––¤–ª–Ø–å–ñ–õ—©—×—à˜]˜®˜¯˜à™j™}™™˜™ö™ýš]šcš…š¡š­Æû›nÖÎÐ¹›u›¥›ªäª›¶›Å›ÎäÍœ™ÉÛž‹žËÑÉŸyìÍŸ|ŸÁÎõŸÖŸÛŸé D J Wê÷ ô öª~«p«}­C®A®Š®¸í¯m¯Ž¯–²G²eíÒµEµKµt¶B¶h·F·j¸v¸”¹•ºm»J¼œ½X¾S¾_¿O¿ˆÀXÀ[ç¥ç²Î¬ÁpÁrÏÛÁwÁxÂ]ÂkÃEÄjÅ’ÅœÆNÆiÆqÈUÈ^ÉšË„Ë‡Ì[ÌˆÍ~Í‚Í†Î’Î•ÎœÏñÂÐtÐzñÇÐ„Ð‘ÐšÑ`ÑvÑ‹ÒAÒ]Ò~Ó~Ó”ÔTÔUÔmÔqÔrÔ„ÕBÕOÕxÖuÖ–×b×g×h×r×‚×”ÚÀÚÖØ[Ø\ØsØŠØ—ÙOÙŒÙ“Ú˜ÛDÛpÝWÝrÝ}ÞTÞjÞvÞ~µüÞÞ–Þ ßzàcátáyá{ÊÍáŒáâNâPârâzãAãBãiãŽãžåWæ„çFèOèîèèŸêdêeêuê‹Á¥ëcïôëìJìaìˆí›îUîVî{î‰ïð†ñkóAó`õkõlöGøCø˜ù€ùù‹ú^úgúsú…úœû@ûkûoûpü]÷îüpýtý~"},
    new string[]{"Yin","ÒòÒýÓ¡ÒøÒôÒûÒõÒþÒñÒ÷ÒüÒúÒðÒùÒóÒöÑÌÜ§Û´à³â¹Ø·ö¸ßÅÛßáþÛóö¯ò¾ë³î÷ñ¿ñ«ä¦ÜáZ]µÖÚðƒBƒÜƒøÌý†‚‡w‡‘‡¨‡à‡ôˆŠ¿Ñˆ¤ˆøÒ¼‹A‹H|•ŸŽ\±ÒJÖ‘@‘\‘€‘‘¶”Õ–@–ð—V™a™ƒ™’™Ó™ýšJšPš’ÒÊ›Ž›ä±œ^œšœÞœôÌ¶¡þž@žô ìªZ«l«­K¯Š°E°a³w´H´€µš¶†¹N»ƒ½s¾ž¿tÜËÆgÇZÉMÊ_ÊaÌaÏPÏrÑPÑÔÓ—ÓÔCÔDÕzÖN×Ú_ÚyÝláDáSâYâiâwãyãŸéœéžêfêŽê”ê›ëLë[ë–ë ì‚ìíï‡ï‹ñ—óSõgúý]ýlý‡"},
    new string[]{"Ying","Ó¦Ó²Ó°ÓªÓ­Ó³Ó¬Ó®Ó¥Ó¢Ó±Ó¨Ó¯Ó¤Ó£Ó§Ó«Ó©ÝÓéºÝöñ¨ÜãðÐëôÝºè¬Û«àÓÞüçøäÞäëÙøó¿å­âßÜþò£‚\‚ŸƒOßì†¦†Ó‡|‡Â‰L‰àÑë‹k‹”‹ë‹ýŒ[ÊŽcŽg_êá‘ª“²”l”t”w•@¾°•£–P–³—@—H—w™Ñ™Õ›s›Æœ€œ»œÁœî}õöžLž]žuž„ž‰ž¡ž­Ÿ‚Ÿ–ŸÉ I«›¬“­‹®O®Zµé°`°Ÿ±j³A´Qµ_·f»Y»k¾x¿I¿MÀKÀtÀ†ÉþÀ”À›ÂmÄ{ÇoÈtËpÌcÍwÎsÎ„ÎžÏ‰ÐNÑšÓLÖh×G×sÙaÚAÜ…³Ñævçè]éAë›ì™í‹íŒîeïI÷jøŠúDúLúˆú—ûKûW"},
    new string[]{"Yo","Ó´Óýà¡†Ñ‡©"},
    new string[]{"Yong","ÓÃÓ¿ÓÀÓµÓ¼ÓÂÓºÓ½Ó¾Ó¶Ó»Ó¸Ó¹Ó·ÓÁÛÕã¼Ù¸Ü­÷«çßà¯ð®÷ÓïÞ‚æ‚ò„Ê†Þ‡‡ˆ¬‰M‹£ÈÝÓb­[~¾Óòú“N“í–º–Ô˜Ÿœ¥KÑžœ œ°M°b³l³‹¶H¹cô§ÆoÉKÞ³ÔÛxÓöàaà{ákäVçOëtî„ïJõ—ö÷I÷‘úx"},
    new string[]{"You","ÓÐÓÖÓÉÓÒÓÍÓÎÓ×ÓÅÓÑÓËÓÇÓÈÓÌÓÕÓÆÓÊÓÏÓÓÓÔÓÄðàØüòÄÝ¬öÏØÕ÷îÝµéàòÊå¶ë»àóèÖòöÝ¯÷øîðòøôíßÏÙ§JŒ‚ºƒžƒÜÌ¾†N†e‡¦Ûê°ÂŠmŠµŒMÞÌŒ²frŽîJMQ‘n‘É‘îÈÅ“AÞí”å–ë—X—`˜A˜©™¢™Ôšü›S›YÇö›w›|›Áœ±HžX ¨ ¶ ûªq«D®h¯_µvµ™¶x¼nÀlÁgÁhÁmÂiÂuñúÃUÃ…Ã‘ÆhÇxÉKÊ~ÍYÍœÑ„Ñ…ÔIÕTØzÝjÝ’Þ”ÞœßKß[ßˆà]à›áRâ™äBäPÐâñfôœõO÷†û~"},
    new string[]{"Yu","ÓëÓÚÓûÓãÓêÓàÓöÓïÓúÓüÓñÓæÓèÓþÓýÓÞÓðÓÝÓéÓÙÓßÓìÓíÓîÓØÓáÓâÓòÓóÓô¹ÈÓõÓÛÓ÷ÓøÓùÓäÖàÓåÎ¾ÓÜÓçÔ¡Ô¢Ô£Ô¤Ô¥Ô¦ÎµåýáÎö§âÅãÐñ¾ðÁæ¥ÞíñÁêìô§ô¨ö¹Ý÷ìÏîÚÚÄæúì¶óÄè¤Ø®í²ì£Ù¶ØñàôðÖâ×êÅÝÇðõÚÍå÷àöðöìÙâÀØ¹ìÛëéáüÝÒòâòõ@¿÷€­‚R‚q‚¦‚øƒhƒ™ƒÊ…P…°…ÇÎáà¡†‰à¯†³†¸†É‡oàÞ‡‰ˆSˆÖˆèˆï‰¥Ï¦°ÂŠÊŠÓŠØ‹U‹V‹‹ž‹äÍðŒ†}áÈ£·îŽZŽ÷ªóƒ„­±‘j°Ã‘µ»ò‘í’G’TÞÖ’§’À” ”Ë”Ñ”ùê¼–fèò–ë–üÎà—™—š—§—å˜@˜K™È™ä™óšQšešu›@›AÎÛãéœMœUœŸœùOÁÄË°ÄžºžÁŸ~ìÐŸú V Œ ¢ªzªÍõ«]«_¬Z¬^¬r­m®Œ®¯_¯°K±E²I²œ³_³†´›µHµNµ€¶R¶r·C·U·{·‹¹zºh»B»Z»n¼u¾s¿›ÁNÁ|Á”ëòÅcÊæÅ„ÆRÔ·Æ‘ÆœÇSÝÎÈgÈhÉfÉ™ÊvÊšÊ ËvÌPÌ]ÍGòÜÎCÎƒÏLÏXÐjÑÃÐsÑ@ÑˆÓDÕZÕ˜ÖIÖ~×uØ‚Ø…Ø‹ÛuÜ†ÝhÝ›ÞXÞ}ßNßyßŽàNàháCáqâDâ•ãƒä`äoå[å“ç~çŸèžé‘é“ãÕê|êœëTëkìMîAîYî„ïJï„ðNð|ñSòeòó^ókôcôdôrô~ôˆõ‚öVöi÷N÷rø\øƒø…øˆùOú}ú–ûCûOû‡ýrý{í±"},
    new string[]{"Yuan","Ô¶Ô±ÔªÔºÔ²Ô­Ô¸Ô°Ô®Ô³Ô¹Ô©Ô´ÔµÔ¬Ô¨Ô·Ô«Ô§Ô¯à÷ö½éÚæÂë¼íóð°ÞòÜ¾ãäè¥ó¢óîÜ«Ûù‚ÓÔÊ„u…Œ…™ÑÊ†T†¿‡…‡ä‡ûˆ@ˆA‰íŠ€Š†‹…‹‹ ‹õÍðŒw…€­¾è–z—¥˜C˜g˜r™´ä¸›ðœYœaœeœmœ®œÆž”Ÿ]ªjªx±\µž´©¸¾‰¿FÁ~ÃOÉAÉVÉdËQËeÍWÍ›ÎQÎmÎzÐcÑjÑrÑ†Ñ“ÖwØ’ÚOÝkÞ@ßRßhß‡ß–áJâƒä‘æ…ÈîÔÉëEîŠñrò{ô’øSøxùtù úMûgüxüŒü"},
    new string[]{"Yue","ÔÂÔ½Ô¼Ô¾ÔÄÀÖÔÀÔÃÔ»ËµÔÁÔ¿å®îáë¾ÙßèÝéÐßÜ†d‡‚àî¿éåùŠx‹íŒéŽ[¦§x‚‘à’F’`’Õ”^•õ–†™µšõË¸Ÿ] q ~«h²ˆ³Eµj¶^¹–ºM»C»a»l»›¼sÄŸÒ©ÌgÍQÍRÍ‘ÍÉó¶ÕfÕhÚŒÚ”õÈÜSÜVÜ‹â_ãXäJä„èpîåÈñé†é‡ó–ûNûVügý›"},
    new string[]{"Yun","ÔÆÔËÔÎÔÊÔÈÔÏÔÉÔÐÔÅÔÌÔÍÔÇÔ±ëµã¢ã³Û©Ü¿óÞè¹êÀáñéæç¡ìÙ»‚Ö„ò…°…Ô†T†½‡ç¾ùŠ@Š[ŠuæÁ‹‹Î¾ÒüÔ¹Áä‘C’d’l•ž–—˜X˜·˜øšŒšè›V›â›éÎÂœÝ·ŸŸ±Ÿ¸Ÿ¾ŸÂ«j®sÎÁ¶Ü±d´p¶n¹S¹oºJ¼‹¾¿A¿Z¿aÀIÀˆÂmÄZÔ·Ç\ÝÒÉCÉQÉlÊ|Ê•ÊŸËœÌNÎQÎ‚ÑŽØ’Ù„ÙšÚOÚSÝ˜ÝœÞdß\àiàyádájâqä]êmëEë…ìBíríyíîfðañNñaýqýy"},
    new string[]{"Za","ÔÓÔÒÕ¦ÔÑÔúÔÛßÆÞÙ†‘ßý†¹àÒ‡m‡Í‡Ô‡ÙŽ‰–ý›e›jžUž£´’Ùá¼’¼™ÅHÅNÒSãNëjësë{íˆô˜"},
    new string[]{"Zad","•õ"},
    new string[]{"Zai","ÔÚÔÙÔÖÔØÔÔÔ×ÔÕçÞáÌ×Ð‚îƒ„²Å’D›’œ…œÖžÄžü²P¿fÇÙ†ÝdáP"},
    new string[]{"Zan","ÔÛÔÝÔÜÔÞô¢ôõôØè¶ÞÙêÃöÉ‚Ìƒ›ƒ­ƒ³†¹‡ÔŒv“S“Ë”€•º–ýä¹äÕžRžUž£­­‘¶`·‰ºdºÅNÒ{×{×“ÙmÙÚŽÛŠà™àŸáAçYçZç‡ç‘ð•"},
    new string[]{"Zang","ÔàÔáÔß²ØÞÊê°æà…M‰ZnãÞ ™ ³ÁnÄ ÅKÊiÙ_ÙjÚEÚNäQñzóGóv"},
    new string[]{"Zao","ÔçÔìÔâÔãÔîÔïÔæÔäÔêÔåÔíÔëÔèÔéßð‚ó†r†×‘V–Ò—_——²ÛŸ¯­F­b°o½Ñ¸Y¸^ºr¿‰ÀRçÒçØÅ²ÝËkÖ×YÚ‹Û›ásè"},
    new string[]{"Ze","ÔòÔðÔñÔóÕ¦²àóåô·àýåÅßõØÆê¾óÐØÓÀ‚È„t²Þ…‹†‡†¨‡K‰÷‹¨¡Ž¾ŽÙŽú’k’¾´ë“ñ•W×õ˜Áštšò›g›zœÚœõÉž•°ƒ²G²c²ž´Ÿ¶ð¢ºjÂdÈ[ÈyÊjÌEÏÒ]Õ‹Ö†Ö‰×yÚØØŸÙ‘ïŽûBý`ýv"},
    new string[]{"Zei","ÔôÏŒÙ\öaöf÷e÷Œöê"},
    new string[]{"Zen","ÔõÚÚÙÔ‡×“Ë×P×U"},
    new string[]{"Zeng","ÔöÔùÔ÷Ôø×ÛçÕîÀêµï­‰ˆ•û™IŸå­Q³D´Œ¾C¿f¿•ÖŸÙ›à‹ä{ôi÷_"},
    new string[]{"Zha","ÔúÕ¨ÔüÕ¢Õ£Õ¥Õ§ÔþÕ©Õ¡ÔýÀ¯²éÕ¤Õ¦íÄðäß¸ßîé«âªòÆÞêà©×õßå÷þ‚´‚¼ƒÔ²á„‘„ž…~¶ß†Æ‡ÍŠLŒoâô’K’s’€’Ÿ’·²å“c“ƒ“’“«“ü–¼–Å˜ÏäÍœÑ¤žÁŸ¤ £®h°•°šóÐ¹€¹†¼’¼™ÂdëúÆzÜÚÊPÊxË ÍlÓuÔpÕ‹ÖŠ×A×QÛzÛ‚ÜˆÞámåŽélëíCõWõ~öl÷‡÷öøýOýeývö´âÇÔû"},
    new string[]{"Zhai","ÕªÕ­Õ®Õ«Õ¯ÔñµÔÕ¬²à¼ÀíÎñ©…‚È‚ù…z…~†‡‰ã¶È’n’Æ“ñ”`”È²ñ˜z™y ¹´Ãóåºj»yëúØŸÔðãSñ~ææódýS"},
    new string[]{"Zhan","Õ¾Õ¼Õ½ÕµÕ´Õ³Õ±Õ¹Õ»Õ²²üÕºÕ¿ÕÀÕ¶Õ·Õ¸Õ°ÚÞÞøì¹×‚·‡~‹¶®ãäøŽEG¬‘é‘ð’€”Ø”ö–î—C—£—ä˜^˜ö™ÙšÖšØäÕå¤¬W°œ±K´D¾`Ç•ËUÌ›ÌœÍtÌ»ÒfÓOÔaÖt×`×d×–Ç«ÚjÛ@Û…õðÝuÝšÞJßá\õ´énë•ïQïsïðeðŒò–ò æöô}÷gø@øZûDûrücür"},
    new string[]{"Zhang","ÕÅÕÂ³¤ÕÊÕÌÕÉÕÆÕÇÕËÕÁÕÈÕÃÕÄÕÍÕÎÕÏØëæÑá¤Ûµè°áÖâ¯ó¯Ÿƒ@‰zŽ¤ŽÇ{ˆ‘P’E³Ð•À›îq¯o¯“²d´˜»w» Ã›ÉŸÙ~ßlçbç•éLéMì ð\ò†÷Jû–"},
    new string[]{"Zhao","ÕÒ×ÅÕÕÕÐÕÖ×¦Õ×³¯ÕÑÕÓÕØ³°ÕÙÕÔèþßúîÈóÉÚ¯Ô†ˆŠ„Ž‚”íêË•×––ÌÒ™˜Ä×åªžÝ Y ªD¬°œ±@²¸S¹|À’Á^ÃAÃDÇŸÖøÔéÔtÖšÚwá“âWãDå™ñqõeøJü{ü…"},
    new string[]{"Zhe","×ÅÕâÕßÕÛÕÚÕÝÕÜÕáÕàÕÞÕãèÏéüô÷ß¡ðÑíÝñÞòØÚØØ±š…z†£†´à¿†øàÖ‡¬‡Ëˆ³¶Â‹«Êü†‘e’V“”Éã”z³â•†•‡–l—‘˜µ˜ÎšyœJž³K³Y»q»„ÞÇÄôÂzÂ™ÍEÏUÏVÐŸñÒÒxÔ€Ö†Ö‘Ö•×y×„ÝWÝmÝtÞHéóß@ßmäOæNÚîñXóCõ„úpúvðºÖø"},
    new string[]{"Zhen","ÕæÕóÕòÕëÕðÕíÕñÕåÕäÕîÕïÕçÕèÕéÕêÕìçÇÝèìõóðéôé»ð¡êâëÞð²ëÓä¥èåî³ÛÚé©‚E‚É´½Ûã‰\‰`ÌîŠª‹ŒzŒÇŽžÉ÷ê¬’r’™“L“Ž”ž”´•_–b–ž–×–Ú—F˜E˜^˜ˆ˜çš‹›l›mœäÚµá›Øª€«‚¬‘±p±w±‡´Uµ¶G¸t»E¼…½G½„¿b¿jÀƒÂrÈZÈœÉRËmÍ–ÐÑ]Ò˜Ô\ÕgÖnØ‘Ùc³ÃÚfÝFÝŸÞtÞßZáGáIá˜â\âœä‹ågåŒæPææ‚ê‡êìkñ}ôIõa÷yößøcülüm¶¦ü‡Ö¡"},
    new string[]{"Zheng","ÕýÕûÕöÕùÕõÕ÷ÕúÖ¤Ö¢Ö£Õü¶¡ÕôÕøÕþá¿îÛï£óÝÚºáçöëØ©ØöÍ‚t„Jˆ½ˆÁ‰^Š’‹o”˜òŽ¬»ÑÕñ‘~³Ð’c’ê’ð“@“Õ³¨•“Ö¹šé›ÆœžÚŸA Žªbî®°Y±k± îª¹~ºP¼l¾PÂtÃwÔ^ÕŠ×CÌËÛtàã`åPô@õSõ›ö]øgÖ¡"},
    new string[]{"Zhi","Ö»Ö®Ö±ÖªÖÆÖ¸Ö½Ö§Ö¥Ö¦ÖÉÖ¨Ö©ÖÊÖ«Ö¬Ö­ÖËÖ¯Ö°ÖÌÖ²µÖÖ³Ö´ÖµÖ¶Ö·ÖÍÖ¹ÖºÖÎÖ¼ÖÏÖ¾Ö¿ÖÀÖÁÖÂÖÃÖÄÊ¶ÖÅÊÏÖÇÖÈàùÞýíéèäè×éòâåìíòÎëùö£Û¤èÙåéÜÆìóåëðºôêõÙëÕæïéùðëÚìõÜïôÛúêÞØ´õ¥õôõÅèÎv~¼¿‚f‚u‚Ž‚À‚Ðƒœ„M„Œ„¬„¶…„†A‡¢ˆ^ˆpˆ€°£ˆÌ‰y‰~¶à‰ïŠ‰Š©ŠÍ‹q‹ÀÊµŒ…ŒªŽŽŽŽÃŽæŽèD¼¿ÃÕáçÊÑd‘e‘p‘Á‘Æ‘ç’W’X’nÍØ’†’”’Ã“w“ˆ“Ÿ“¯“´”S”T”`”Õ”òêÇ•y–s–y–»–ñÔÔ—d—„—Ð—ù˜u˜—˜µ˜Þ˜à™£™±šlØµ›D›E›b›‚›œ›±œ]œíœþZ†žž\ŸÜ ÃªOªa­}­•®‡¯F¯U¯W¯€±‚³UµYÊ¾Æîµoµwµ…µ•¶A¶_¶h¶o¶q¶~»ý¶ƒ¶ˆ¶ž·W·a¹e¼ˆ¼•½‚½¿@¿{¿—Á“êÈÂpÂšÃeÃqÄˆÅ\Å]ÆWÆ‡ÆÇ ËSËŒÌuÌŒÍVÏHÏdÐ}ÐÐ—Ð˜ÑuÒjÒžÓdÓhÓzÔJÕIÖ}×RØTØUØ Ù|Ù—ÛNÛyÛÛ•Û—ÜUÜWÜÝTÝXÝe³ÙÞŒßgßtÛªáBá™âŸã‡äKäkèeÌúèœé@êeênêuë\ëbñ\ñcñ‹òsòòŽöSøFøTøvøúEúvð¯ü~âº"},
    new string[]{"Zhong","ÖÐÖØÖÖÖÓÖ×ÖÚÖÕÖÑÖÒÖÙÖÔõàô±ó®ïñÚ£âìZ«‚£„d†Áˆú‰VŠqŠt‹gŒ»Žº³×–°šp›O›wäüžÆŸŽ ð¯~±Šµr·N·rÍ¯¹W»b½K¾…Ä[ô©Æ ¶­ÊWÍ\ò¼ÎuÎ ÏxÐ\ÐxÐ{Ñ~ÖAÛ Þ‰â`â{äVæRçŠø‚ü™âº"},
    new string[]{"Zhou","ÖÜÖÞÖåÖàÖÝÖáÖÛÖçÖèÖæÖßÖâÖãÖäôíëÐæûÝ§ßúíØç§ô¦æ¨ôüÆÙªúÙÃƒuƒÙ…â†B×Ä†µà¹‡€‡œ‹B¸®ÅÅ¤’ô•ƒ•ŽèÖ—¹×¢›œ@žëžö«‰®L¯J°™±T²H³B×£¹»N»Q»‹¼q¼—¿UÁŸÃiô¶ÈFÈ’ËgÔkÔ—Õ{ÕŒÖa×p×žÚÁµ÷ÙkÚQÝSÝcÝqÞbÖðßLàXâ™ã{ÓËë“ñtñ™òLò|óEæãå÷öBù@ûb"},
    new string[]{"Zhu","×¡Ö÷ÖíÖñÖêÖóÖþÖüÖýÖöÖô×¢×£×¤ÊôÊõÖéÖõÖëÖìÖùÖîÖïÖðÖúÖòÖûäóä¨ØùðñôãÜïÜÑéÍô¶èÌóçìÄÙªîùðæä¾ñÒõî÷æÛ¥éÆóÃÓèÐ„Ÿ„±„¸†B†ø‡€‡Úˆ|‰£‰ÔŒFÄþŒeŒ¥ŒÙ­Êü“o”±”½¶·”á•ô³¯–’Äû–Çèú˜Ö™·™½™Á™îšŸ›{Å¢äøîžzž¯žÛŸ— T ‰±v²š³d³p´„µ‚¶‹·”¸m¸‰¸˜¹hºBºZºa¼Ÿ½A½ZÀ‚ÁCÁqÆ^Ær×ÂÇAÇdÉÊxË\ËŸË òÄÎwÏŽÐEÐWÑNÔ]Ô}ÕDÖTØiÙAÚŸÛBÛHÝOÞŽ¶ºßIãIãLãäŠèTè“×è³ýê•ë—ïŒñ[ñvñ–ò|æãõfö^÷Eø–úžû„ü}ÖøØ¼"},
    new string[]{"Zhua","×¥×¦ÎÎ“«“ë™tºœÄó˜"},
    new string[]{"Zhuai","×§×ªŒ¾’Å±‘ÛJî“àÜ"},
    new string[]{"Zhuan","×ª×¨×©×¬´«×«×­ò§âÍßùãç‚÷ƒQƒ]„–…¡‡Êˆæ‰t‹§ŒNŒŸŒ£wÞÒ“»ÍÄ`žÀ¬ƒ­A®U´u¸|ºeºiº‹»M¿xÂZÄRÄxÉEÏmÒNÖK×NÙÜžÞDàî…ð‚÷H"},
    new string[]{"Zhuang","×°×²×¯×³×®×´´±×±Ù×ÞÊí°ãÜ‰Ñ‰ÕŠyŠÏŽáã¿‘Þ‘ß—[˜¶œ³rŸ` îª‘»’¼Pô¾ÇPÇfÑbÚCÚM¸Ó"},
    new string[]{"Zhui","×·×¹×º×¶×¸×µæíã·çÄö¿‚…´¹ˆ§‰‹ŠÜ´§é³›d®I®•³›´q´œ¸¹Š¾Y¿PÄJÄiÖÂÝÈÕ…Ù˜ÞVáWá^åFåMåYæmèVê ËíðUòKùx"},
    new string[]{"Zhun","×¼×»ÍÍëÆñ¸ƒý†”ˆSˆÍŒd÷•H´¾œ·œÊ®líï¶›¼ƒ¾M´¿ëÓÐqÔRÕÞ„ï‚â½ün"},
    new string[]{"Zhuo","×½×À×Å×Ä×¾×Æ×Ç×¿×Á½É×Â×ÃßªìÌåªÚÂä·äÃÙ¾ïíìúí½Q„†„ŸÉ×…¬†à¨‡€ˆVˆp‰~ŠƒŠß°’Á¶Þ“â“ð”½”Ù”Ú”Û”Þ•Œ—z—‡—¬èþ—Á˜‘™·šõÄ×œÊáž•žãŸO æªK¬k²ž³˜·q·‡·Ÿ¸BóçºW»S»mÀUÁMÂvëÆÉ|Þ©ËyÎ[Ï—ÐXÕ}ÕŽÖ‘Ú}õÀõÖÛ•õîãnärè@èCùhú|è¼Öø"},
    new string[]{"Zi","×Ö×Ô×Ó×Ï×Ñ×Ê×ËÖ¨×Ò×Ð×È×É×Î×Õ×Ì×ÍóÊôÒö·ïöí§ÚÑôôç»è÷ööïÅæÜñèõþ÷ÚêßÜëö¤áÑíöæ¢ê¢ÊÂ‚•„…»…èßÚ††êŠ—ŠœŒIŒU –j–ã—Â˜h™U´Î›d›››œ¹nÐ ¼«R­uçÞ´Ã±{³Iµ›¶f¶‡·T·}¼|¾lÃcÃhÃuÆTÆ†Æ“ÆÇÈŒÉ›ËFÍIÔ`ÖJÙDÙYÚaÚƒÚÝdÝwÝ–ÔØàtâBâˆä\åOæSætÐ¿éCîoîpõ™ö‹ùƒüˆýRýUÆëýb"},
    new string[]{"Zo","…ø†€"},
    new string[]{"Zong","×Ü×Ý×Ú×Ø×Û×Ù××ÙÌôÕèÈëê‚~‚‚ôˆî¸¾ÙÄ¼Èß’Ö“K“i“¨•f–Q—Þ˜º|ƒœŸÐŸÙ Qª`ªf¯S¯—³Ÿ´†·O¼F¾C¾h¾t¾‘¾›¿G¿k¿v¿‚ÂCÅ‹È É~ÉÎxØqÛrÛ™åSæCçEèQòRòióWôAôiöRö`"},
    new string[]{"Zou","×ß×á×à×ÞöíÛ¸ÚîæãÚÁ‚¸‹ƒ’ô“o×å—¯—°é¨¹t¾jÆcÇˆÕŒÚ[àYàuò|õ•öOüPýwýåÁ"},
    new string[]{"Zu","×é×å×ã×è×â×æ×çÝÏïß×äÙÞºÇ‚y‚ú…a†XßýàÒŒþŒœáÞI–¼¾Ú´ã •«~³^·B¹Œ¼½M¾\ÜÚÈ{ÉaÔ{ÖŠôõÚŽÛnÛ€õíâžãIãJå@æcæŽæ—èì†îxñzæà"},
    new string[]{"Zuan","×ê×ë×¬çÚõòß¬„®“S´éÔÜ”€™çºe»gÀFÀjÀyÙÜgèjè"},
    new string[]{"Zui","×î×ì×í×ï¶Ñ¾×õþÞ©…‰†÷‡’‹¥éêŽT´Ý´é•–K–˜–è˜§˜á™d™i™Þû­r²Bµ‘·B·s½SÀxôÈÃÏ`ÞfáEáPáUäŽå@ëh"},
    new string[]{"Zun","×ð×ñ÷®ß¤é×ƒQƒV‡g‰–µìý’Ž’Ä’Û–çžˆ¿ŸÀ–ÑI×JÛIÛZ¶×ã†ç÷VùŒú•"},
    new string[]{"Zuo","×ö×÷×ø×ó×ù×òÔä×Á´é×ôóÐõ¡ßòìñëÑâôÚè×õÕ§‚F…øŒõŒö´ì’Û–Ã—½íÄ¶}¶š¹i¼d¿–ÆzÇgÈyÈzÉÐŠÕ‹´×â—èïŽàÜÚâ"}
   };
        #endregion


        /// <summary>
        /// ×ª»»³ÉÐ¡Ð´£¬ÒÆ³ýÆäËû·ûºÅ£¬Ã»ÓÐ·Ö¸ô·û
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Convert(string str)
        {
            return Convert(str, "", false, true);
        }

        /// <summary>
        /// ×ª»»³ÉÐ¡Ð´£¬ÒÆ³ýÆäËû·ûºÅ£¬×Ô¶¨Òå·Ö¸ô·û
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Convert(string str,string split)
        {
            return Convert(str, split, false, true);
        }

        /// <summary>
        /// ½«ºº×Ö×Ö·û´®×ª»»³ÉÆ´Òô×Ö·û´®
        /// </summary>
        /// <param name="str">Òª×ª»»µÄºº×Ö</param>
        /// <param name="split">·Ö¸î·ûºÅ</param>
        /// <param name="firstUp">Ê××ÖÄ¸ÊÇ·ñ´óÐ´</param>
        /// <param name="removeOther">ÊÇ·ñÒÆ³ýÆäËû</param>
        /// <returns></returns>
        public static string Convert(string str, string split = "", bool firstUp = true, bool removeOther = false)
        {
            if (str == null)
                return null;
            Encoding ed = Encoding.GetEncoding("GB2312");
            if (ed == null)
                throw (new ArgumentException("Ã»ÓÐÕÒµ½±àÂë¼¯GB2312"));
            int bh = 0;
            char[] charary = str.ToCharArray();
            byte[] bAry = new byte[2];
            StringBuilder rtnSb = new StringBuilder();
            for (int i = 0; i < charary.Length; i++)
            {

                bAry = ed.GetBytes(charary[i].ToString());
                if (bAry.Length == 1)
                {
                    rtnSb.Append(charary[i]);
                    continue;
                }
                bh = bAry[0] - 0xA0;
                if (0x10 <= bh && bh <= 0x57)//ÊÇgb2312ºº×Ö
                {
                    bool isFind = false;
                    for (int j = 0; j < _Allhz.Length; j++)
                    {
                        if (_Allhz[j][1].IndexOf(charary[i]) != -1)
                        {
                            rtnSb.Append(_Allhz[j][0]);
                            isFind = true;
                            break;
                        }
                    }
                    if (!isFind)
                        rtnSb.Append(charary[i]);

                }
                else if (!removeOther)
                    rtnSb.Append(charary[i]);


                //ÊÇ·ñÌí¼Ó·Ö¸î·ûºÅ
                if (!string.IsNullOrEmpty(split))
                    rtnSb.Append(split);
            }

            //É¾³ý×îºóÒ»¸ö·Ö¸ô·û
            if (!string.IsNullOrEmpty(split) && rtnSb.Length > 1)
                rtnSb.Remove(rtnSb.Length - 1, 1);

            if (!firstUp) //×ª»»³ÉÐ¡Ð´
                return rtnSb.ToString().Replace("/", "").ToLower();

            return rtnSb.ToString().Replace("/","");
        }

    }
}