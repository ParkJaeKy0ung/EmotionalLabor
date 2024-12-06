using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.WinControls;
using Telerik.WinControls.UI.Localization;

namespace NBOGUN
{
    public class RadMessageBoxLocalization : RadMessageLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            //Get localized string, if null use "base" texts (english)
            return RadLocalizationGeneric.GetRadMessageBoxLocalizedString(id) ?? base.GetLocalizedString(id);
        }
    }

    public class MyRadGridViewLoalization : RadGridLocalizationProvider
    {
        public override string GetLocalizedString(string id)
        {
            //Get localized string, if null use "base" texts (english)
            return RadLocalizationGeneric.GetRadGridLocalizedString(id) ?? base.GetLocalizedString(id);
        }
    }
    //public class RadTreeViewLocalization : RadTreeViewLocalizationProvider
    //{
    //    public override string GetLocalizedString(string id)
    //    {
    //        //Get localized string, if null use "base" texts (english)
    //        return RadLocalizationGeneric.GetRadTreeViewLocalizedString(id) ?? base.GetLocalizedString(id);
    //    }
    //}
    //public class RadDockLocalization :  RadDockLocalizationProvider
    //{
    //    public override string GetLocalizedString(string id)
    //    {
    //        //Get localized string, if null use "base" texts (english)
    //        return RadLocalizationGeneric.GetRadDockLocalizedString(id) ?? base.GetLocalizedString(id);
    //    }
    //}
    //public class RadGridLocalization : RadGridLocalizationProvider
    //{
    //    public override string GetLocalizedString(string id)
    //    {
    //        //Get localized string, if null use "base" texts (english)
    //        return RadLocalizationGeneric.GetRadGridLocalizedString(id) ?? base.GetLocalizedString(id);
    //    }
    //}

    /// <summary>
    /// Translations Functions...
    /// </summary>
    /// <remarks>
    /// All stuff here should be placed in ResX files (standard format for localization!)
    /// For the demo I put strings here :o)
    ///</remarks>
    internal static class RadLocalizationGeneric
    {
        /// <summary>
        /// RadMessageBox translations
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Localized string or null if not overloaded here</returns>
        internal static string GetRadMessageBoxLocalizedString(string id)
        {
            switch (System.Globalization.CultureInfo.CurrentCulture.LCID)
            {
                case 1036:
                    #region French
                    switch (id)
                    {
                        case RadMessageStringID.AbortButton: return "Annuler";
                        case RadMessageStringID.CancelButton: return "Annuler";
                        case RadMessageStringID.IgnoreButton: return "Ignorer";
                        case RadMessageStringID.NoButton: return "Non";
                        case RadMessageStringID.OKButton: return "Ok";
                        case RadMessageStringID.RetryButton: return "Réessayer";
                        case RadMessageStringID.YesButton: return "Oui";
                    }
                    #endregion
                    break;
                case 1031:
                    #region German
                    switch (id)
                    {
                        case RadMessageStringID.AbortButton: return "Abbrechen";
                        case RadMessageStringID.CancelButton: return "Abbrechen";
                        case RadMessageStringID.IgnoreButton: return "Ignorieren";
                        case RadMessageStringID.NoButton: return "Nein";
                        case RadMessageStringID.OKButton: return "OK";
                        case RadMessageStringID.RetryButton: return "Wiederholen";
                        case RadMessageStringID.YesButton: return "Ja";
                    }
                    #endregion
                    break;
                case 1040:
                    #region Italian
                    switch (id)
                    {
                        case RadMessageStringID.AbortButton: return "Annulla";
                        case RadMessageStringID.CancelButton: return "Annulla";
                        case RadMessageStringID.IgnoreButton: return "Ignora";
                        case RadMessageStringID.NoButton: return "No";
                        case RadMessageStringID.OKButton: return "OK";
                        case RadMessageStringID.RetryButton: return "Riprova";
                        case RadMessageStringID.YesButton: return "Sì";
                    }
                    #endregion
                    break;
                case 1042://한국
                    #region 한국
                    switch (id)
                    {
                        case RadMessageStringID.AbortButton: return "중지";
                        case RadMessageStringID.CancelButton: return "취소";
                        case RadMessageStringID.IgnoreButton: return "무시";
                        case RadMessageStringID.NoButton: return "아니오";
                        case RadMessageStringID.OKButton: return "확인";
                        case RadMessageStringID.RetryButton: return "다시";
                        case RadMessageStringID.YesButton: return "예";
                    }
                    #endregion
                    break;
                case 1033:
                //English
                default:
                    //Others
                    break;
            }
            //Returns null when language not overriden or when string not translated
            return null;
        }

        /// <summary>
        /// TreeView translations
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Localized string or null if not overloaded here</returns>
        //internal static string GetRadTreeViewLocalizedString(string id)
        //{
        //    switch (System.Globalization.CultureInfo.CurrentCulture.LCID)
        //    {
        //        case 1036:
        //            #region French
        //            switch (id)
        //            {
        //                case RadTreeViewStringId.ContextMenuCollapse:
        //                    return "Réduire";
        //                case RadTreeViewStringId.ContextMenuDelete:
        //                    return "Supprimer";
        //                case RadTreeViewStringId.ContextMenuEdit:
        //                    return "Editer";
        //                case RadTreeViewStringId.ContextMenuExpand:
        //                    return "Déplier";
        //                case RadTreeViewStringId.ContextMenuNew:
        //                    return "Nouveau";
        //            }
        //            #endregion
        //            break;
        //        case 1031:
        //            #region German
        //            switch (id)
        //            {
        //                case RadTreeViewStringId.ContextMenuCollapse:
        //                    return "Reduzieren";
        //                case RadTreeViewStringId.ContextMenuDelete:
        //                    return "Löschen";
        //                case RadTreeViewStringId.ContextMenuEdit:
        //                    return "Umbenennen";
        //                case RadTreeViewStringId.ContextMenuExpand:
        //                    return "Erweitern";
        //                case RadTreeViewStringId.ContextMenuNew:
        //                    return "Neu";
        //            }
        //            #endregion
        //            break;
        //        case 1042:
        //            #region 한국
        //            switch (id)
        //            {
        //                case RadTreeViewStringId.ContextMenuCollapse:
        //                    return "종료";
        //                case RadTreeViewStringId.ContextMenuDelete:
        //                    return "삭제";
        //                case RadTreeViewStringId.ContextMenuEdit:
        //                    return "수정";
        //                case RadTreeViewStringId.ContextMenuExpand:
        //                    return "확장";
        //                case RadTreeViewStringId.ContextMenuNew:
        //                    return "메뉴";
        //            }
        //            #endregion
        //            break;
        //        case 1033:
        //        //English
        //        default:
        //            //Others
        //            break;
        //    }
        //    //Returns null when language not overriden or when string not translated
        //    return null;
        //}

        /// <summary>
        /// RadDock translations
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Localized string or null if not overloaded here</returns>
        //internal static string GetRadDockLocalizedString(string id)
        //{
        //    switch (System.Globalization.CultureInfo.CurrentCulture.LCID)
        //    {
        //        case 1036:
        //            #region French
        //            switch (id)
        //            {
        //                case RadDockStringId.ContextMenuAutoHide:
        //                    return "Masquer Automatiquement";
        //                case RadDockStringId.ContextMenuCancel:
        //                    return "Annuler";
        //                case RadDockStringId.ContextMenuClose:
        //                    return "Fermer";
        //                case RadDockStringId.ContextMenuCloseAll:
        //                    return "Fermer tout";
        //                case RadDockStringId.ContextMenuCloseAllButThis:
        //                    return "Fermer tout sauf celui-ci";
        //                case RadDockStringId.ContextMenuDockable:
        //                    return "Accrochable";
        //                case RadDockStringId.ContextMenuFloating:
        //                    return "Flottant";
        //                case RadDockStringId.ContextMenuHide:
        //                    return "Masquer";
        //                case RadDockStringId.ContextMenuMoveToNextTabGroup:
        //                    return "Déplacer vers l'onglet suivant";
        //                case RadDockStringId.ContextMenuMoveToPreviousTabGroup:
        //                    return "Déplacer vers l'onglet précédent";
        //                case RadDockStringId.ContextMenuNewHorizontalTabGroup:
        //                    return "Nouveau groupe horizontal";
        //                case RadDockStringId.ContextMenuNewVerticalTabGroup:
        //                    return "Nouveau groupe vertical";
        //                case RadDockStringId.ContextMenuTabbedDocument:
        //                    return "Mettre le document en onglet"; //TODO: check translation...
        //            }
        //            #endregion
        //            break;
        //        case 1031:
        //            #region German
        //            switch (id)
        //            {
        //                case RadDockStringId.ContextMenuAutoHide:
        //                    return "Automatisch im Hintergrund";
        //                case RadDockStringId.ContextMenuCancel:
        //                    return "Abbrechen";
        //                case RadDockStringId.ContextMenuClose:
        //                    return "Schließen";
        //                case RadDockStringId.ContextMenuCloseAll:
        //                    return "Alle schließen";
        //                case RadDockStringId.ContextMenuCloseAllButThis:
        //                    return "Alle außer diesem schließen";
        //                case RadDockStringId.ContextMenuDockable:
        //                    return "Andockbar";
        //                case RadDockStringId.ContextMenuFloating:
        //                    return "Unverankert";
        //                case RadDockStringId.ContextMenuHide:
        //                    return "Ausblenden";
        //                case RadDockStringId.ContextMenuMoveToNextTabGroup:
        //                    return "In nächste Registerkartengruppe verschieben";
        //                case RadDockStringId.ContextMenuMoveToPreviousTabGroup:
        //                    return "In vorherige Registerkartengruppe verschieben";
        //                case RadDockStringId.ContextMenuNewHorizontalTabGroup:
        //                    return "Neue horizontale Registerkartengruppe";
        //                case RadDockStringId.ContextMenuNewVerticalTabGroup:
        //                    return "Neue vertikale Registerkartengruppe";
        //                case RadDockStringId.ContextMenuTabbedDocument:
        //                    return "Dokument im Registerkartenformat";
        //            }
        //            #endregion
        //            break;
        //        case 1040:
        //            #region Italian
        //            switch (id)
        //            {
        //                case RadDockStringId.ContextMenuAutoHide:
        //                    return "Nascondi automaticamente";
        //                case RadDockStringId.ContextMenuCancel:
        //                    return "Annulla";
        //                case RadDockStringId.ContextMenuClose:
        //                    return "Chiudi";
        //                case RadDockStringId.ContextMenuCloseAll:
        //                    return "Chiudi tutto";
        //                case RadDockStringId.ContextMenuCloseAllButThis:
        //                    return "Chiudi tutti tranne questo";
        //                case RadDockStringId.ContextMenuDockable:
        //                    return "Ancorabile";
        //                case RadDockStringId.ContextMenuFloating:
        //                    return "Flottante";
        //                case RadDockStringId.ContextMenuHide:
        //                    return "Nascondi";
        //                case RadDockStringId.ContextMenuMoveToNextTabGroup:
        //                    return "Passa alla scheda successiva";
        //                case RadDockStringId.ContextMenuMoveToPreviousTabGroup:
        //                    return "Passa alla scheda precedente";
        //                case RadDockStringId.ContextMenuNewHorizontalTabGroup:
        //                    return "Nuovo gruppo orizzontale";
        //                case RadDockStringId.ContextMenuNewVerticalTabGroup:
        //                    return "Nuovo gruppo verticale";
        //                case RadDockStringId.ContextMenuTabbedDocument:
        //                    return "Metti il documento nella scheda";
        //            }
        //            #endregion
        //            break;
        //        case 1033:
        //        //English
        //        default:
        //            //Others
        //            break;
        //    }
        //    //Returns null when language not overriden or when string not translated
        //    return null;
        //}

        /// <summary>
        /// GridView translations
        /// </summary>
        /// <param name="id">Id of item</param>
        /// <returns>Localized string or null if not overloaded here</returns>
        internal static string GetRadGridLocalizedString(string id)
        {
            switch (System.Globalization.CultureInfo.CurrentCulture.LCID)
            {
                case 1033:
                case 1042://한국
                    switch (id)
                    {
                        case RadGridStringId.AddNewRowString:
                            return "새로운 행을 추가하시려면 클릭해 주세요";
                        case RadGridStringId.BestFitMenuItem:
                            return "최적의 열 너비";
                        case RadGridStringId.ClearSortingMenuItem:
                            return "정렬 초기화";
                        case RadGridStringId.ClearValueMenuItem:
                            return "값 삭제";
                        case RadGridStringId.ColumnChooserFormCaption:
                            return "열 선택";
                        case RadGridStringId.ColumnChooserFormMessage:
                            return "열을 숨기려면,\n" +
                                             "테이블에서 옮기다\n" +
                                             "이 창에서";
                        case RadGridStringId.ColumnChooserMenuItem:
                            return "열 표시";
                        case RadGridStringId.ConditionalFormattingBtnAdd:
                            return "새 규칙";
                        case RadGridStringId.ConditionalFormattingBtnApply:
                            return "적용";
                        case RadGridStringId.ConditionalFormattingBtnCancel:
                            return "중단";
                        case RadGridStringId.ConditionalFormattingBtnOK:
                            return "확인";
                        case RadGridStringId.ConditionalFormattingChooseOne:
                            return "[선택]";
                        case RadGridStringId.ConditionalFormattingBtnRemove:
                            return "규칙 삭제";
                        case RadGridStringId.ConditionalFormattingCaption:
                            return "조건부 서식 편집기";
                        case RadGridStringId.ConditionalFormattingChkApplyToRow:
                            return "전체 라인에 규칙 적용";
                        case RadGridStringId.ConditionalFormattingContains:
                            return "포함 [값1]";
                        case RadGridStringId.ConditionalFormattingDoesNotContain:
                            return "포함되지 않음 [값1]";
                        case RadGridStringId.ConditionalFormattingEndsWith:
                            return "끝 [값1]";
                        case RadGridStringId.ConditionalFormattingEqualsTo:
                            return "Ist gleich [값1]";
                        case RadGridStringId.ConditionalFormattingGrpConditions:
                            return "Regeln";
                        case RadGridStringId.ConditionalFormattingGrpProperties:
                            return "Eigenschaften der Regel";
                        case RadGridStringId.ConditionalFormattingIsBetween:
                            return "[값 1]과 [값 2] 사이에 있음";
                        case RadGridStringId.ConditionalFormattingIsGreaterThan:
                            return "[값 1]보다 큽니다";
                        case RadGridStringId.ConditionalFormattingIsGreaterThanOrEqual:
                            return "[값 1]보다 크거나 같음";
                        case RadGridStringId.ConditionalFormattingIsLessThan:
                            return "작은 [값 1]";
                        case RadGridStringId.ConditionalFormattingIsLessThanOrEqual:
                            return "[값 1]보다 작거나 같음";
                        case RadGridStringId.ConditionalFormattingIsNotBetween:
                            return "[값 1]과 [값 2] 사이가 아님";
                        case RadGridStringId.ConditionalFormattingIsNotEqualTo:
                            return "[값 1]과 같지 않음";
                        case RadGridStringId.ConditionalFormattingLblColumn:
                            return "셀만 포맷";
                        case RadGridStringId.ConditionalFormattingLblName:
                            return "규칙 이름 :";
                        case RadGridStringId.ConditionalFormattingLblType:
                            return "셀 값 :";
                        case RadGridStringId.ConditionalFormattingLblValue1:
                            return "값 1 :";
                        case RadGridStringId.ConditionalFormattingLblValue2:
                            return "값 2 :";
                        case RadGridStringId.ConditionalFormattingMenuItem:
                            return "조건부 서식";
                        case RadGridStringId.ConditionalFormattingRuleAppliesOn:
                            return "규칙 적용 대상 :";
                        case RadGridStringId.ConditionalFormattingStartsWith:
                            return "[Value1]로 시작합니다.";
                        case RadGridStringId.CopyMenuItem:
                            return "사본";
                        case RadGridStringId.CustomFilterDialogBtnCancel:
                            return "중단";
                        case RadGridStringId.CustomFilterDialogBtnOk:
                            return "확인";
                        case RadGridStringId.CustomFilterDialogCaption:
                            return "사용자 정의 필터링 대화 상자";
                        case RadGridStringId.CustomFilterDialogLabel:
                            return "적용되는 라인 표시 :";
                        case RadGridStringId.CustomFilterDialogRbAnd:
                            return "과";
                        case RadGridStringId.CustomFilterDialogRbOr:
                            return "또는";
                        case RadGridStringId.CustomFilterMenuItem:
                            return "맞춤 필터";
                        case RadGridStringId.DeleteRowMenuItem:
                            return "행 삭제";
                        case RadGridStringId.EditMenuItem:
                            return "편집";
                        case RadGridStringId.FilterFunctionBetween:
                            return "사이 ...";
                        case RadGridStringId.FilterFunctionContains:
                            return "포함...";
                        case RadGridStringId.FilterFunctionCustom:
                            return "맞춤 필터";
                        case RadGridStringId.FilterFunctionDoesNotContain:
                            return "포함하지 않음 ...";
                        case RadGridStringId.FilterFunctionEndsWith:
                            return "끝 ...";
                        case RadGridStringId.FilterFunctionEqualTo:
                            return "평등하다 ...";
                        case RadGridStringId.FilterFunctionGreaterThan:
                            return "더 큰 것 ...";
                        case RadGridStringId.FilterFunctionGreaterThanOrEqualTo:
                            return "크거나 같음 ...";
                        case RadGridStringId.FilterFunctionIsEmpty:
                            return "비어 있습니다 ...";
                        case RadGridStringId.FilterFunctionIsNull:
                            return "null 입니다 ...";
                        case RadGridStringId.FilterFunctionLessThan:
                            return "보다 작습니다 ...";
                        case RadGridStringId.FilterFunctionLessThanOrEqualTo:
                            return "작거나 같음 ...";
                        case RadGridStringId.FilterFunctionNoFilter:
                            return "필터 없음";
                        case RadGridStringId.FilterFunctionNotBetween:
                            return "사이가 아니라 ...";
                        case RadGridStringId.FilterFunctionNotEqualTo:
                            return "평등하지 않다 ...";
                        case RadGridStringId.FilterFunctionNotIsEmpty:
                            return "비어 있지 않습니다 ...";
                        case RadGridStringId.FilterFunctionNotIsNull:
                            return "null이 아닙니다 ...";
                        case RadGridStringId.FilterFunctionStartsWith:
                            return "시작 ...";
                        case RadGridStringId.GroupByThisColumnMenuItem:
                            return "이 열의 그룹화";
                        case RadGridStringId.GroupingPanelDefaultMessage:
                            return "이 열로 그룹화하려면 여기로 열을 드래그하십시오";
                        case RadGridStringId.HideMenuItem:
                            return "열을 숨 깁니다";
                        case RadGridStringId.NoDataText:
                            return "데이터 없음";
                        case RadGridStringId.PasteMenuItem:
                            return "삽입";
                        case RadGridStringId.PinMenuItem:
                            return "열 수정";
                        case RadGridStringId.SortAscendingMenuItem:
                            return "오름차순으로 정렬";
                        case RadGridStringId.SortDescendingMenuItem:
                            return "내림차순 정렬";
                        case RadGridStringId.UngroupThisColumn:
                            return "그룹 삭제";
                        case RadGridStringId.UnpinMenuItem:
                            return "고정 제거";
                    }
                    break;
                //English
                default:
                    //Others
                    break;
            }
            //Returns null when language not overriden or when string not translated
            return null;
        }
    }
}
