using UnityEngine;
using PeepBo.Utils;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using PeepBo.Managers;
using UnityEngine.SceneManagement;
using Naninovel;
using System.Threading.Tasks;

namespace PeepBo.UI.Popup
{
    public class UI_HaerokdoPopup : UI_Popup
    {
        public string[] explanations = {
            "��ġ: ���ѹα� �泲 �¾�\n����: 0.9km<size=30>2</size>\n�α���: 50��\n\n- ���������� �泲 �¾ȱ� �α� ��\n\n- �طϵ��� �� �� �ִ� ��� ���ǵ����� �����Ͽ� �ѹ� ���� 7�ÿ� �����ϸ� �� 1�ð� ���� �ҿ�ȴ�\n\n- �����忡�� ��밡 �ϳ� ������ ���� ���� �Ǿ��ְ� ���������� �ֹε��� ���ư��� �ô� ��\n\n- ������ ���� ���� �� �߾����� �� ���� ��簡 ���ϴ�.",
            "\n- ������ �����̴�.\n\n- ��ϼ����� ����� ���º��� Ư�������� �����ƴ�.\n\n- �湮���� ���� ���ѵǾ�������, �Ϲ� �湮���� ���� �㰡�� �־�߸� ������ �� �ִ�.\n\n- ���ڼ��� �� �޿� �ѹ� ��° �� ȭ���Ͽ� ���� �湮�Ѵ�.\n\n- �������� Ÿ���� ����� �ҹ� ������ �ô޸� ſ�� �������� �ص��� ����Ѵ�.",
            "\n- ������ ������ ������ �㿡�� ���� �� ���� �ʰ� ���ε ���� ���� �������� �޺��� ��� �ֹε� ��� ū ������ ���� ��� �ִ�.\n\n- �����ü��� ���ű��� ������������ �� �߾ӿ� ��ġ���ִ�\n\n- ���������� ��ϴ� ������ ���п� ������ܿ��� �طϵ��� �������� �Ŀ��� ���� �ϰ� �ִ�.\n\n- ���ڼ��� �������� �׸� ǳ������ �ʾ� ����ǰ�� �Ǹ����� �ʰ� �ֹε鳢�� ��������.",
            "\n- ���İ� ���� ��� ��� ����� ��� ������ ���� ������ ��κ� �ڱ������ϰ� �ִ�.\n\n- �ֹ����縦 ���� 1�⿡ �� �� �İ߿��� ���������� ������ �湮��"
        };
        enum Texts
        {
            explanation,
        }
        enum GameObjects
        {
            CloseButton,
            list,
            before,
            after,
        }
        private void Start()
                    => Init();

        public override void Init()
        {
            base.Init();
            BindObjects();

            TextMeshProUGUI exp = GetTextPro((int)Texts.explanation);
            exp.text = explanations[0];
        }

        private void BindObjects()
        {
            Bind<GameObject>(typeof(GameObjects));
            Bind<TextMeshProUGUI>(typeof(Texts));

            GameObject closeButton = GetObject((int)GameObjects.CloseButton);
            AddUIEvent(closeButton, OnClickCloseButton, Define.UIEvent.Click);
            AddButtonAnim(closeButton);

            GameObject tolist = GetObject((int)GameObjects.list);
            AddUIEvent(tolist, OnClickListButton, Define.UIEvent.Click);
            AddButtonAnim(tolist);

            GameObject tobefore = GetObject((int)GameObjects.before);
            AddUIEvent(tobefore, OnClickBeforeButton, Define.UIEvent.Click);
            AddButtonAnim(tobefore);

            GameObject toafter = GetObject((int)GameObjects.after);
            AddUIEvent(toafter, OnClickAfterButton, Define.UIEvent.Click);
            AddButtonAnim(toafter);

        }

        private void OnClickListButton(PointerEventData evt)
        {
            ClosePopupUI();
        }
        private void OnClickCloseButton(PointerEventData evt)
        {
            CloseAllPopupUI();
        }
        private void OnClickBeforeButton(PointerEventData evt)
        {
            TextMeshProUGUI exp = GetTextPro((int)Texts.explanation);
            int textIdx = explanations.IndexOf(exp.text.ToString());

            if (textIdx <= 0)
            {
                textIdx = explanations.Length;
            }
            textIdx--;
            exp.text = explanations[textIdx];
        }
        private void OnClickAfterButton(PointerEventData evt)
        {
            TextMeshProUGUI exp = GetTextPro((int)Texts.explanation);
            int textIdx = explanations.IndexOf(exp.text.ToString());

            if (textIdx >= explanations.Length - 1)
            {
                textIdx = -1;
            }
            textIdx++;
            exp.text = explanations[textIdx];
        }
    }
}